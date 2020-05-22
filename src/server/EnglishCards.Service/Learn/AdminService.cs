using EnglishCards.Model;
using System;
using System.Collections.Generic;
using System.Text;
using DBModel = EnglishCards.Model.Data;

namespace EnglishCards.Service.Learn
{
    public class AdminService
    {
        private DataContext _dataContext;

        public AdminService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        //public async Task<GroupsResponse> GetGroups(RequestContext requestContext)
        //{

        //}
    }
}
