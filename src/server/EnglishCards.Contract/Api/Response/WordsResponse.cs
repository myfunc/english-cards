using EnglishCards.Contract.Api.Response.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishCards.Contract.Api.Response
{
    public class WordsResponse : BaseResponse
    {
        public IEnumerable<Word> Words { get; set; }
    }
}
