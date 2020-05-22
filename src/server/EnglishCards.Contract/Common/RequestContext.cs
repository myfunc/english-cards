using EnglishCards.Contract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishCards.Contract
{
    public class RequestContext
    {
        public Guid UserId { get; set; }
        public string Login { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public IEnumerable<Group> Groups { get; set; }
        public string IP { get; set; }
    }
}
