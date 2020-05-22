using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishCards.Contract.Api.Response.Data
{
    public class Word
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NativeTranslation { get; set; }
        public IEnumerable<WordTranslation> ForeignTranslations { get; set; }
        public string ImageUrl { get; set; }
    }
}
