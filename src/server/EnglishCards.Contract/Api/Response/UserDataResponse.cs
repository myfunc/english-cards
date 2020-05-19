using EnglishCards.Contract.Api.Response.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishCards.Contract.Api.Response
{
    public class UserDataResponse : BaseResponse
    {
        public UserData UserData { get; set; }
    }
}
