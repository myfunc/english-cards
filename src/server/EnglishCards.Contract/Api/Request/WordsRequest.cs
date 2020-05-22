using EnglishCards.Contract.Api.Request.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishCards.Contract.Api.Request
{
    public class WordsRequest
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public WordRequestMode Mode { get; set; }

        public Guid? GroupId { get; set; }

        public Pagination Pagination { get; set; } = new Pagination();
    }
}
