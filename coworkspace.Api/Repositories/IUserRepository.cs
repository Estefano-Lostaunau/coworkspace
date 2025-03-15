using Coworkspace.Api.Models;

namespace Coworkspace.Api.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserById(int id);
        Task<User?> GetUserByEmail(string email);
        Task AddUser(User user);
    }
}