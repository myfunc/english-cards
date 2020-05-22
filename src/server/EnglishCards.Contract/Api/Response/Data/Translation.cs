using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishCards.Contract.Api.Response.Data
{
    public class WordTranslation
    {
        public string Translation { get; set; }
        public string LangCode { get; set; }
        public int Priority { get; set; }
    }
}
