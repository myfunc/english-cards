using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishCards.Model.Repositories
{
    public class DataRepository
    {
        private readonly DataContext _dataContext;
        public DataRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
    }
}
