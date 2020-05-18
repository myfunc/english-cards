using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishCards.Contract.Api.Response.Data
{
    public class Error
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
