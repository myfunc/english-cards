using EnglishCards.Contract.Api.Response.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishCards.Contract.Api.Response
{
    public class WordsResponse : BaseResponse
    {
        public string NativeLangCode { get; set; }

        public string ForeignLangCode { get; set; }

        public IEnumerable<Word> Words { get; set; }
    }
}
