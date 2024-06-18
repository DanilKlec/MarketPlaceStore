using NewsStore.Core.Models;

namespace NewsStore.API.Contacts
{
    public record UsersRequest(
            Guid Id,
            string userName,
            string name,
            string password,
            Role role,
        string roleName);
}

