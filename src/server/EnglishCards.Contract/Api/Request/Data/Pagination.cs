using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishCards.Contract.Api.Request.Data
{
    public class Pagination
    {
        public int Top { get; set; } = 20;
        public int Skip { get; set; } = 0;
    }
}
