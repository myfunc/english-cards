using EnglishCards.Model.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection.Metadata;
using System.Text;

namespace EnglishCards.Model
{
    public static class Constants
    {
        public static class Users
        {
            public static readonly Guid Admin = new Guid("52ee4399-b1cb-43eb-9842-792cfc4a7f89");
        }

        public static class Groups
        {
            public static readonly Guid Admin = new Guid("1d528109-9edf-404a-9607-e2b207e17a74");
            public static readonly Guid User = new Guid("7c1e1967-716f-4851-b237-cb5479076b8b");
        }
    }

    public static class DataSeeder
    {
        public static void SeedData(DataContext context)
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
                        NativeLanguage = languages[1]
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
    }
}
