using NewsStore.Core.Models;

namespace NewsStore.Core.Abstractions
{
    public interface IUsersRepository
    {
        Task<Guid> Create(Users t);
        Task<Guid> Delete(Guid id);
        Task<List<Users>> Get();
        Task<Guid> Update(Guid id, string userName, string name, string password, Role role);
    }
}