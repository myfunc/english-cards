using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnglishCards.Contract.Api.Request;
using EnglishCards.Contract.Api.Response;
using EnglishCards.Service.Learn;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EnglishCards.Host.Controllers
{
    [ApiController]
    [Route("api")]
    public class LearnController : ControllerBase
    {
        private readonly ILogger<LearnController> _logger;
        private LearnService _learnService;

        public LearnController(ILogger<LearnController> logger, LearnService learnService)
        {
            _logger = logger;
            _learnService = learnService;
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
    }
}
