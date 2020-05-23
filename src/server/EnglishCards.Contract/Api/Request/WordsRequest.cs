using EnglishCards.Contract.Api.Request.Data;
using EnglishCards.Contract.Api.Request.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EnglishCards.Contract.Api.Request
{
    public class WordsRequest
    {
       // [JsonConverter(typeof(StringEnumConverter))]
        public WordRequestMode Mode { get; set; }

        public Guid? GroupId { get; set; }

        public Pagination Pagination { get; set; } = new Pagination();
    }
}
