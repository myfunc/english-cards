using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EnglishCards.Model.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly DataContext _context;
        public BaseRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
