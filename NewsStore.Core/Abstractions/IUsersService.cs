using NewsStore.Core.Models;

namespace NewsStore.Application.Services
{
    public interface IUsersService
    {
        Task<Guid> CreateUser(Users user);
        Task<Guid> DeleteUser(Guid id);
        Task<List<Users>> GetAllUsers();
        Task<Guid> UpdateUser(Guid id, string userName, string name, string password, Role role);
    }
}