using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishCards.Contract.Common
{
    public class Group
    {
        public Guid Id { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public string Name { get; set; }
    }
}
