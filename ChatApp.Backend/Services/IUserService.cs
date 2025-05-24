using ChatApp.Models;

namespace ChatApp.Services
{
    public interface IUserService
    {
        Task<User> GetByIdAsync(string userId);
        Task<User> GetByNameAsync(string userName);
        Task<List<User>> GetAllUsersAsync();
        Task<bool> CreateUserAsync(User user);
        Task<bool> UpdateUserContentAsync(string userId, User updatedUser);
        Task<bool> DeleteMessageAsync(string userId);

    }
}
