using Microsoft.EntityFrameworkCore;
using NewsStore.Core.Abstractions;
using NewsStore.Core.Models;
using NewsStore.DataBase.Entites;

namespace NewsStore.DataBase.Reposotories
{
    public class TidingsRepository : ITidingsRepository
    {
        private readonly TidingsStoreDbContext _context;

        public TidingsRepository(TidingsStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tidings>> Get()
        {
            var newsEntities = await _context.Tidings
                .AsNoTracking()
                .ToListAsync();

            var users = newsEntities.Select(e => e.Users).ToList();

            var news = newsEntities
                .Select(e => Tidings.Get(e.Id, e.Number, e.Description, e.Name, e.Rating, e.UsersId, e.Picture).Tidings)
                .ToList();

            return news;
        }

        public async Task<Guid> Create(Tidings t)
        {
            var tidingsEntites = new TidingsEntites()
            {
                Id = Guid.NewGuid(),
                Name = t.Name,
                Number = t.Number,
                Description = t.Description,
                Rating = t.Rating,
                DateCreate = DateTime.Now,
                Picture = t.Picture,
            };

            if (t.Users != null)
                tidingsEntites.UsersId = t.Users.Id;

            await _context.Tidings.AddAsync(tidingsEntites);
            await _context.SaveChangesAsync();

            return tidingsEntites.Id;
        }

        public async Task<Guid> Update(Guid id, int? number, string description, string name, int rating, Users? users, string picture)
        {
            var db = _context.Tidings
                 .FirstOrDefault(_ => _.Id == id);

            db.Name = name;
            db.Number = number;
            db.Description = description;
            db.Rating = rating;
            db.Picture = picture;

            if (users != null)
                db.UsersId = users.Id;

            await _context.SaveChangesAsync();

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            var db = _context.Tidings.FirstOrDefault(_ => _.Id == id);

            if (db != null)
                _context.Remove(db);

            await _context.SaveChangesAsync();

            return id;
        }
    }
}
