using ChatApp.Models;
using ChatApp.Repository;

namespace ChatApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetByIdAsync(string userId)
        {
            Guid userIdGuid = Guid.Parse(userId);
            return await _userRepository.GetByIdAsync(userIdGuid);
        }
        public async Task<User> GetByNameAsync(string userName)
        {
            return await _userRepository.GetByNameAsync(userName);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            return await _userRepository.CreateUserAsync(user);
        }

        public async Task<bool> UpdateUserContentAsync(string userId, User updatedUser)
        {
            Guid userIdGuid = Guid.Parse(userId);
            return await _userRepository.UpdateUserAsync(updatedUser, userIdGuid);
        }

        public async Task<bool> DeleteMessageAsync(string userId)
        {
            Guid userIdGuid = Guid.Parse(userId);
            return await _userRepository.DeleteUserAsync(userIdGuid);
        }
    }
}
