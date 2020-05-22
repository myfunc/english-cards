using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnglishCards.Contract;
using EnglishCards.Contract.Api.Request;
using EnglishCards.Contract.Api.Response;
using EnglishCards.Host.Services;
using EnglishCards.Service.Learn;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EnglishCards.Host.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize]
    public class LearnController : ControllerBase
    {
        private LearnService _learnService;
        private RequestContext _requestContext;

        public LearnController(LearnService learnService, RequestContextService requestContextService)
        {
            _learnService = learnService;
            _requestContext = requestContextService.RequestContext;
        }

        [HttpGet("groups")]
        public GroupsResponse GetGroups()
        {
            return _learnService.GetGroups();
        }

        [HttpPost("words")]
        public WordsResponse GetWords([FromBody] WordsRequest request)
        {
            return _learnService.GetWords(request);
        }

        [HttpPost("debug")]
        public string Debug()
        {
            return string.Join(",", User.Claims.Select(p => $"{p.Type} = {p.Value}"));
        }

        //[HttpGet("userData")]
        //public WordsResponse GetUserData()
        //{
        //    return _learnService.GetUserData(request);
        //}

        //[HttpPost("commit")]
        //public WordsResponse Commit([FromBody] CommitRequest request)
        //{
        //    return _learnService.Commit(request);
        //}
    }
}
