using System;

namespace EnglishCards.Model
{
    public static class Constants
    {
        public static class Users
        {
            public static readonly Guid Admin = new Guid("52ee4399-b1cb-43eb-9842-792cfc4a7f89");
        }

        public static class Groups
        {
            public static readonly Guid Admin = new Guid("1d528109-9edf-404a-9607-e2b207e17a74");
            public static readonly Guid User = new Guid("7c1e1967-716f-4851-b237-cb5479076b8b");
        }
    }
}
