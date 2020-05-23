using EnglishCards.Contract;
using EnglishCards.Contract.Api.Request;
using EnglishCards.Contract.Api.Request.Enums;
using EnglishCards.Contract.Api.Response;
using EnglishCards.Contract.Api.Response.Data;
using EnglishCards.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBModel = EnglishCards.Model.Data;

namespace EnglishCards.Service.Learn
{
    public class LearnService
    {
        private DataContext _dataContext;

        public LearnService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<GroupsResponse> GetGroups(RequestContext requestContext)
        {
            var user = await _dataContext.Users
                .Include("UserInGroup.Group")
                .FirstAsync(p => p.Id == requestContext.UserId);

            var progressData = GetUserProgress(user);

            var groupSets = _dataContext.GroupSets;

            var groupsInResponse = groupSets
                .AsEnumerable()
                .Select(p => new Group()
            {
                Id = p.Id,
                Name = p.Name,
                PreviewImageUrl = p.PreviewImageUrl,
                TotalWords = p.WordInGroupSets.Count,
                LearnedWords = progressData == null ? 0 : p.WordInGroupSets
                    .Where(w => progressData.WordsProgress.Any(wp => wp.IsLearned && wp.Word.Id == w.WordId))
                    .Count()
            }).ToList();

            return new GroupsResponse()
            {
                Groups = groupsInResponse
            };
        }

        public async Task<WordsResponse> GetWords(WordsRequest request, RequestContext requestContext)
        {
            var user = await _dataContext.FindAsync<DBModel.User>(requestContext.UserId);
            
            List<DBModel.Word> dbWords = new List<DBModel.Word>();

            if (request.Mode == WordRequestMode.Group)
            {
                dbWords = _dataContext.WordInGroupSets
                    .Where(p => p.GroupSetId == request.GroupId)
                    .Select(p => p.Word)
                    .Skip(request.Pagination.Skip)
                    .Take(request.Pagination.Top).ToList();
            }

            if (request.Mode == WordRequestMode.Learned)
            {
                var progressData = GetUserProgress(user);
                if (progressData != null)
                {
                    dbWords = progressData.WordsProgress
                        .Where(p => p.IsLearned)
                        .Select(p => p.Word)
                        .Skip(request.Pagination.Skip)
                        .Take(request.Pagination.Top).ToList();
                }
            }

            if (request.Mode == WordRequestMode.All)
            {
                dbWords = _dataContext.Words
                    .Skip(request.Pagination.Skip)
                    .Take(request.Pagination.Top).ToList();
            }

            if (dbWords == null)
            {
                return new WordsResponse()
                {
                    IsSuccess = false,
                    Error = new Error()
                    {
                        Code = 200,
                        Message = "Incorect Mode"
                    }
                };
            }

            if (dbWords.Count == 0)
            {
                return new WordsResponse()
                {
                    NativeLangCode = user.NativeLanguage.Code,
                    ForeignLangCode = user.ForeignLanguage.Code,
                    Words = Enumerable.Empty<Word>()
                };
            }

            var filteredWords = dbWords
                .Where(p =>
                    p.WordTranslations.Any(t => t.Language.Code == user.ForeignLanguage.Code) &&
                    p.WordTranslations.Any(t => t.Language.Code == user.NativeLanguage.Code))
                .ToList();

            List<Word> responseWords = filteredWords.Select(p => new Word()
            {
                Id = p.Id,
                ImageUrl = p.ImageUrl,
                Name = p.Name,
                NativeTranslation = p.WordTranslations
                    .OrderByDescending(t => t.Translation)
                    .First(t => t.Language.Code == user.NativeLanguage.Code)
                    .Translation,
                ForeignTranslations = p.WordTranslations
                    .Where(t => t.Language.Code == user.ForeignLanguage.Code)
                    .Select(t => new WordTranslation()
                    {
                        LangCode = t.Language.Code,
                        Priority = t.Priority,
                        Translation = t.Translation
                    })
            }).ToList();

            return new WordsResponse()
            {
                NativeLangCode = user.NativeLanguage.Code,
                ForeignLangCode = user.ForeignLanguage.Code,
                Words = responseWords
            };
        }

        public async Task<UserDataResponse> GetUserData(RequestContext requestContext)
        {
            var user = await _dataContext.Users.FirstAsync(p => p.Id == requestContext.UserId);
            return new UserDataResponse()
            {
                UserData = new UserData()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Login = user.Login,
                    NativeLangCode = user.NativeLanguage.Code,
                    ForeignLangCode = user.ForeignLanguage.Code
                }
            };
        }

        public async Task<CommitResponse> CommitWords(CommitRequest request, RequestContext requestContext)
        {
            var user = await _dataContext.FindAsync<DBModel.User>(requestContext.UserId);
            var progressData = GetUserProgress(user);

            if (progressData == null)
            {
                progressData = new DBModel.ProgressData()
                {
                    Language = user.ForeignLanguage,
                    WordsProgress = new List<DBModel.ProgressDataWord>()
                };
                if (user.ProgressData == null)
                {
                    user.ProgressData = new List<DBModel.ProgressData>();
                }
                user.ProgressData.Add(progressData);
            }

            //await _dataContext.SaveChangesAsync();

            // user = await _dataContext.FindAsync<DBModel.User>(requestContext.UserId);
            // progressData = GetUserProgress(user);

            foreach (var commitWord in request.Words)
            {
                var progressWord = progressData.WordsProgress.FirstOrDefault(p => 
                    p.Word.Id == commitWord.WordId && 
                    p.Language.Code == user.ForeignLanguage.Code);
                if (progressWord == null)
                {
                    DBModel.Word commitedWord = await _dataContext.Words.FindAsync(commitWord.WordId);
                    if (commitedWord != null)
                    {
                        var newProgressWord = new DBModel.ProgressDataWord()
                        {
                            Id = Guid.NewGuid(),
                            LearnAttempts = commitWord.LearnAttempts,
                            IsLearned = commitWord.IsLearned,
                            Word = commitedWord,
                            Language = user.ForeignLanguage
                        };
                        _dataContext.Add(newProgressWord);
                        progressData.WordsProgress.Add(newProgressWord);
                    }
                } 
                else
                {
                    progressWord.IsLearned = commitWord.IsLearned;
                    progressWord.LearnAttempts = commitWord.LearnAttempts + progressWord.LearnAttempts;
                }
            }

            await _dataContext.SaveChangesAsync();

            return new CommitResponse();
        }

        private DBModel.ProgressData GetUserProgress(DBModel.User user)
        {
            return user.ProgressData?.FirstOrDefault(p => p.Language == user.ForeignLanguage);
        }
    }
}
