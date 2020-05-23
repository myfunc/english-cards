using EnglishCards.Model.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EnglishCards.Model.Repositories
{
    public class SystemRepository : BaseRepository
    {
        public SystemRepository(DataContext dataContext) : base(dataContext) { }

        public async Task<Language> GetLanguageByCode(string code)
        {
            return await _context.Languages.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Code == code);
        }
    }
}
