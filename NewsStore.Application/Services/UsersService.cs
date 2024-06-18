using NewsStore.Core.Abstractions;
using NewsStore.Core.Models;

namespace NewsStore.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _repository;

        public UsersService(IUsersRepository usersRepository)
        {
            _repository = usersRepository;
        }

        public async Task<List<Users>> GetAllUsers()
        {
            return await _repository.Get();
        }

        public async Task<Guid> CreateUser(Users user)
        {
            return await _repository.Create(user);
        }

        public async Task<Guid> UpdateUser(Guid id, string userName, string name, string password, Role role)
        {
            return await _repository.Update(id, userName, name, password, role);
        }

        public async Task<Guid> DeleteUser(Guid id)
        {
            return await _repository.Delete(id);
        }
    }
}
