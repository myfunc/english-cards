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
        public string Tags { get; set; }
        public IEnumerable<Guid> Groups { get; set; }
        public string IP { get; set; }
    }
}
