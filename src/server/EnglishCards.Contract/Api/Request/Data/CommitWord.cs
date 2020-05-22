using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishCards.Contract.Api.Request.Data
{
    public class CommitWord
    {
        public Guid WordId { get; set; }
        public int LearnAttempts { get; set; }
        public bool IsLearned { get; set; }
    }
}
