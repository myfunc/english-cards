using EnglishCards.Model.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;

namespace EnglishCards.Model
{
    public static class DataSeeder
    {
        public static void SeedSystemData(DataContext context)
        {
            var languages = new Language[]
                {
                    new Language()
                    {
                        Code = "en",
                        Name = "English"
                    },
                    new Language()
                    {
                        Code = "ru",
                        Name = "Russian"
                    }
                };

            var users = new User[]
                {
                    new User {
                        Id = Constants.Users.Admin,
                        Login = "Admin",
                        Password = "Aa123456",
                        Email = "mail@example.com",
                        Tags = "System",
                        NativeLanguage = languages[1],
                        ForeignLanguage = languages[0]
                    }
                };

            var groups = new Group[]
                {
                    new Group {
                        Id = Constants.Groups.Admin,
                        Name = "Admin",
                        Tags = "System"
                    },
                    new Group {
                        Id = Constants.Groups.User,
                        Name = "User",
                        Tags = "System"
                    },
                };

            var userInGroups = new UserInGroup[]
                {
                    new UserInGroup()
                    {
                        UserId = Constants.Users.Admin,
                        GroupId = Constants.Groups.Admin
                    }
                };

            context.AddRange(languages);
            context.AddRange(users);
            context.AddRange(groups);
            context.AddRange(userInGroups);
            context.SaveChanges();
        }

        public static void SeedTestData(DataContext context)
        {
            int wordIndex = 0;
            var enLang = context.Languages.Find("en");
            var ruLang = context.Languages.Find("ru");

            int groupCountSeed = 9;

            context.AddRange(Enumerable.Range(0, 4)
                .Select(n => new GroupSet()
                {
                    Id = Guid.NewGuid(),
                    Name = "Group_" + n.ToString(),
                    WordInGroupSets = Enumerable.Range(0, groupCountSeed++)
                        .Select(n => new WordInGroupSet()
                        {
                            Word = new Word()
                            {
                                Id = Guid.NewGuid(),
                                Name = "Word_" + wordIndex++.ToString(),
                                WordTranslations = Enumerable.Range(0, 5)
                                .Select(t => new WordTranslation()
                                {
                                    Id = Guid.NewGuid(),
                                    Priority = t,
                                    Language = t % 2 == 0 ? enLang : ruLang,
                                    Translation = "Translation_" + t.ToString()
                                }).ToList()
                            }
                        }).ToList()
                }).ToList());


            //context.AddRange(Enumerable.Range(0, 12)
            //    .Select(n => new Word()
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Word_" + wordIndex++.ToString(),
            //        WordTranslations = Enumerable.Range(0, 5)
            //            .Select(t => new WordTranslation()
            //            {
            //                Id = Guid.NewGuid(),
            //                Priority = t,
            //                Language = t % 2 == 0 ? enLang : ruLang,
            //                Translation = "Translation_" + t.ToString()
            //            }).ToList()
            //    }).ToList());

            context.SaveChanges();
        }
    }
}
