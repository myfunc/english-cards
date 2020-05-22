using EnglishCards.Contract;
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
            var headers = accessor.HttpContext.Request.Headers;

            var userId = accessor.HttpContext.User?.Identity;
            
            if (userId == null)
            {
                return;
            }

            RequestContext = new RequestContext();
        }
    }
}
