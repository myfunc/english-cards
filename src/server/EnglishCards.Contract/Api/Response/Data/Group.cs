using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishCards.Contract.Api.Response.Data
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int TotalWords { get; set; }
        public int LearnedWords { get; set; }
        public string PreviewImageUrl { get; set; }
    }
}
