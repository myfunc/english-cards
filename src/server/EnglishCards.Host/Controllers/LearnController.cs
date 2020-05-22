using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnglishCards.Contract;
using EnglishCards.Contract.Api.Request;
using EnglishCards.Contract.Api.Response;
using EnglishCards.Contract.Api.Response.Data;
using EnglishCards.Host.Services;
using EnglishCards.Service.Learn;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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

        [HttpGet("getGroups")]
        public async Task<GroupsResponse> GetGroups()
        {
            return await _learnService.GetGroups(_requestContext);
        }

        [HttpPost("getWords")]
        public async Task<WordsResponse> GetWords([FromBody] WordsRequest request)
        {
            return await _learnService.GetWords(request, _requestContext);
        }

        [HttpPost("debug")]
        public string Debug()
        {
            return JsonConvert.SerializeObject(_requestContext);
        }

        [HttpGet("getUserData")]
        public async Task<UserDataResponse> GetUserData()
        {
            return await _learnService.GetUserData(_requestContext);
        }

        [HttpPost("commitWords")]
        public async Task<CommitResponse> CommitWords([FromBody] CommitRequest request)
        {
            return await _learnService.CommitWords(request, _requestContext);
        }
    }
}
