using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishCards.Contract.Api.Response.Data
{
    public class Word
    {
        public Guid Id { get; set; }
        public string NativeTranslation { get; set; }
        public string ForeignTranslation { get; set; }
        public string ImageUrl { get; set; }
    }
}
