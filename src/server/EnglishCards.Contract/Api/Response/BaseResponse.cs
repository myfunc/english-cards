using EnglishCards.Contract.Api.Response.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishCards.Contract.Api.Response
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; } = true;
        public Error Error { get; set; }
    }
}
