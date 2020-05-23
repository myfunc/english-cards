using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishCards.Model.Repositories
{
    public class DataRepository : BaseRepository
    {
        public DataRepository(DataContext dataContext) : base(dataContext) { }
    }
}
