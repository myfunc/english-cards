using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishCards.Contract.Api.Request
{
    public class WordsRequest
    {
        public string Mode { get; set; }
        public Guid? GroupId { get; set; }
    }
}
