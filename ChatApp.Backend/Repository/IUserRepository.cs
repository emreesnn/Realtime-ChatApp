using ChatApp.Models;

namespace ChatApp.Repository
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid userId);
        Task<User> GetByNameAsync(string userName);
        Task<List<User>> GetAllAsync();
        Task<bool> CreateUserAsync(User user);
        Task<bool> UpdateUserAsync(User updatedUser, Guid userId);
        Task<bool> DeleteUserAsync(Guid userId);
    }
}
