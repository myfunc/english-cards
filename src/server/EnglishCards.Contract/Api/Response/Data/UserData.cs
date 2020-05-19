using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishCards.Contract.Api.Response.Data
{
    public class UserData
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
    }
}
