using EnglishCards.Contract;
using EnglishCards.Contract.Api.Request;
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
            var user = await _dataContext.FindAsync<DBModel.User>(requestContext.UserId);
            var progressData = user.ProgressData.FirstOrDefault(p => p.Language == user.ForeignLanguage);

            var groupSets = _dataContext.GroupSets;

            var groupsInResponse = groupSets.Select(p => new Group()
            {
                Id = p.Id,
                Name = p.Name,
                PreviewImageUrl = p.PreviewImageUrl,
                TotalWords = p.WordInGroupSets.Count,
                LearnedWords = p.WordInGroupSets
                    .Where(w => progressData.WordsProgress
                        .Where(wp => wp.IsLearned)
                        .Any(wp => wp.Word.Id == w.WordId))
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
            var progressData = user.ProgressData.FirstOrDefault(p => p.Language == user.ForeignLanguage);

            List<DBModel.Word> dbWords = null;

            if (request.Mode == WordRequestMode.Group)
            {
                dbWords = _dataContext.Words
                    .Where(p => p.WordInGroupSets.Any(w => w.GroupSetId == request.GroupId))
                    .Take(request.Pagination.Top)
                    .Skip(request.Pagination.Skip).ToList();
            }

            if (request.Mode == WordRequestMode.Learned)
            {
                dbWords = _dataContext.Words
                    .Where(p => progressData.WordsProgress.Any(wp => wp.Id == p.Id && wp.IsLearned))
                    .Take(request.Pagination.Top)
                    .Skip(request.Pagination.Skip).ToList();
            }

            if (request.Mode == WordRequestMode.All)
            {
                dbWords = _dataContext.Words
                    .Take(request.Pagination.Top)
                    .Skip(request.Pagination.Skip).ToList();
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

            var filteredWords = dbWords
                .Where(p =>
                    p.WordTranslations.Any(t => t.Language.Code == user.ForeignLanguage.Code) &&
                    p.WordTranslations.Any(t => t.Language.Code == user.NativeLanguage.Code))
                .ToList();

            List<Word> responseWords = filteredWords.Select(p => new Word()
            {
                Id = p.Id,
                ImageUrl = p.ImageUrl,
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
            var progressData = user.ProgressData.FirstOrDefault(p => p.Language == user.ForeignLanguage);

            foreach (var commitWord in request.Words)
            {
                var progressWord = progressData.WordsProgress.FirstOrDefault(p => 
                    p.Word.Id == commitWord.WordId && 
                    p.Language.Code == user.ForeignLanguage.Code);
                if (progressWord == null)
                {
                    var newProgressWord = new DBModel.ProgressDataWord()
                    {
                        Id = Guid.NewGuid(),
                        LearnAttempts = commitWord.LearnAttempts,
                        IsLearned = commitWord.IsLearned,
                        Word = new DBModel.Word() { Id = commitWord.WordId },
                        Language = user.ForeignLanguage
                    };
                    progressData.WordsProgress.Add(newProgressWord);
                } else
                {
                    progressWord.IsLearned = commitWord.IsLearned;
                    progressWord.LearnAttempts = commitWord.LearnAttempts + progressWord.LearnAttempts;
                }
            }

            await _dataContext.SaveChangesAsync();

            return new CommitResponse();
        }
    }
}
