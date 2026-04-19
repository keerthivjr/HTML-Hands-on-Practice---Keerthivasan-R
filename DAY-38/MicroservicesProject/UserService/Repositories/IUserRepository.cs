using Shared.Models;

namespace UserService.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserById(int id);
        Task<User?> GetUserByUsername(string username);
        Task<User?> GetUserByEmail(string email);
        Task<User> CreateUser(User user);
        Task<bool> UserExists(string username, string email);
    }
}