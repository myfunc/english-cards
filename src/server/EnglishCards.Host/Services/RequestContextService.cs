using EnglishCards.Contract;
using EnglishCards.Contract.Common;
using EnglishCards.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishCards.Host.Services
{
    public class RequestContextService
    {
        public RequestContext RequestContext { get; }

        public RequestContextService(DataContext dataContext, IHttpContextAccessor accessor)
        {
            var httpContext = accessor.HttpContext;

            var userId = accessor.HttpContext.User?.Identity?.Name;
            
            if (userId == null)
            {
                return;
            }

            var user = dataContext.Users.Find(new Guid(userId));
            if (user == null)
            {
                return;
            }

            RequestContext = new RequestContext()
            {
                IP = httpContext.Connection.LocalIpAddress.ToString(),
                Login = user.Login,
                UserId = user.Id,
                Tags = user.GetTags(),
                Groups = user.UserInGroup.Select(p => new Group()
                {
                    Id = p.Group.Id,
                    Name = p.Group.Name,
                    Tags = p.Group.GetTags()
                }).ToList()
            };
        }
    }
}
