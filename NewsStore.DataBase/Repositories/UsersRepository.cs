using Microsoft.EntityFrameworkCore;
using NewsStore.Core.Abstractions;
using NewsStore.Core.Models;
using NewsStore.DataBase.Entites;

namespace NewsStore.DataBase.Reposotories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly TidingsStoreDbContext _context;

        public UsersRepository(TidingsStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Users>> Get()
        {
            var newsEntities = await _context.Users
                .AsNoTracking()
                .ToListAsync();

            var news = newsEntities
                .Select(e => Users.Create(e.Id, e.UserName, e.Name, e.Password, e.RoleId.Value).Users)
                .ToList();

            return news;
        }

        public async Task<Guid> Create(Users t)
        {
            var userEntites = new UsersEntites() { Id = t.Id, UserName = t.UserName, Name = t.Name, Password = t.Password, RoleId = t.RoleId };

            await _context.Users.AddAsync(userEntites);
            await _context.SaveChangesAsync();

            return userEntites.Id;
        }

        public async Task<Guid> Update(Guid id, string userName, string name, string password, Role role)
        {
            var db = _context.Users
                 .FirstOrDefault(_ => _.Id == id);

            db.UserName = userName;
            db.Name = name;
            db.Password = password;

            if (role != null)
                db.RoleId = role.Id;

            await _context.SaveChangesAsync();

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            var db = _context.Users.FirstOrDefault(_ => _.Id == id);

            if (db != null)
                _context.Users.Remove(db);

            await _context.SaveChangesAsync();

            return id;
        }
    }
}
