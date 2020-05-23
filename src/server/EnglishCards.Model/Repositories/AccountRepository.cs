using EnglishCards.Model.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishCards.Model.Repositories
{
    public class AccountRepository : BaseRepository
    {
        public AccountRepository(DataContext dataContext):base(dataContext){ }

        public async Task<User> GetUserByLoginPassword(string login, string password)
        {
            return await _context.Users.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Login == login && p.Password == password);
        }

        public async Task<User> GetUser(Guid id, IEnumerable<string> includeColumns = null)
        {
            var query = _context.Users.AsNoTracking();
            foreach (var column in includeColumns)
            {
                query.Include(column);
            }
            return await query.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> IsUserExists(string login, string email)
        {
            return await _context.Users.AnyAsync(p => p.Login == login || p.Email == email);
        }

        public async void AddUser(User newUser)
        {
            var langs = await _context.Languages.ToListAsync();
            newUser.NativeLanguage = langs.First(p => p.Code == newUser.NativeLanguage.Code);
            newUser.ForeignLanguage = langs.First(p => p.Code == newUser.ForeignLanguage.Code);
            _context.Users.Add(newUser);
        }

        public void GrantUser(Guid userId, Guid groupId)
        {
            _context.UserInGroups.Add(new UserInGroup()
            {
                UserId = userId,
                GroupId = groupId
            });
        }
    }
}
