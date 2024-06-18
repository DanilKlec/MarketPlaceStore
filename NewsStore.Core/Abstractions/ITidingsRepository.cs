using NewsStore.Core.Models;

namespace NewsStore.Core.Abstractions
{
    public interface ITidingsRepository
    {
        Task<Guid> Create(Tidings t);
        Task<Guid> Delete(Guid id);
        Task<List<Tidings>> Get();
        Task<Guid> Update(Guid id, int? number, string description, string name, int rating, Users? user, string picture);
    }
}