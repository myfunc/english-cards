using EnglishCards.Contract.Api.Request;
using EnglishCards.Contract.Api.Response;
using EnglishCards.Contract.Api.Response.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishCards.Service.Learn
{
    public class LearnService
    {
        public GroupsResponse GetGroups()
        {
            return new GroupsResponse()
            {
                Groups = new[]
                {
                    new Group()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Apple base",
                        LearnedWords = 10,
                        TotalWords = 23
                    },
                    new Group()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Medicine base",
                        LearnedWords = 2,
                        TotalWords = 10
                    }
                }
            };
        }

        public WordsResponse GetWords(WordsRequest request)
        {
            return new WordsResponse()
            {
                Words = new[]
                {
                    new Word()
                    {
                        Id = Guid.NewGuid(),
                        NativeTranslation = "Яблоко",
                        ForeignTranslation = "Apple"
                    },
                    new Word()
                    {
                        Id = Guid.NewGuid(),
                        NativeTranslation = "Дыня",
                        ForeignTranslation = "Melon"
                    },
                    new Word()
                    {
                        Id = Guid.NewGuid(),
                        NativeTranslation = "Банан",
                        ForeignTranslation = "Banana"
                    }
                }
            };
        }
    }
}
