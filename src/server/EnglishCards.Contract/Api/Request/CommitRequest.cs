using EnglishCards.Contract.Api.Request.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishCards.Contract.Api.Request
{
    public class CommitRequest
    {
        public IEnumerable<CommitWord> Words { get; set; }
    }
}
