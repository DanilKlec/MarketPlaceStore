using NewsStore.Core.Models;

namespace NewsStore.Core.Abstractions
{
    public interface ITidingsService
    {
        Task<Guid> CreateTidings(Tidings t);
        Task<Guid> DeleteTidings(Guid id);
        Task<List<Tidings>> GetAllTidings();
        Task<Guid> UpdateTidings(Guid id, int? number, string description, string name, int rating, Users? user, string picture);
    }
}