using NewsStore.Core.Abstractions;
using NewsStore.Core.Models;

namespace NewsStore.Application.Services
{
    public class TidingsService : ITidingsService
    {
        private readonly ITidingsRepository _repository;

        public TidingsService(ITidingsRepository tidingsRepository)
        {
            _repository = tidingsRepository;
        }

        public async Task<List<Tidings>> GetAllTidings()
        {
            return await _repository.Get();
        }

        public async Task<Guid> CreateTidings(Tidings t)
        {
            return await _repository.Create(t);
        }

        public async Task<Guid> UpdateTidings(Guid id, int? number, string description, string name, int rating , Users? user, string picture)
        {
            return await _repository.Update(id, number, description, name, rating, user, picture);
        }

        public async Task<Guid> DeleteTidings(Guid id)
        {
            return await _repository.Delete(id);
        }
    }
}
