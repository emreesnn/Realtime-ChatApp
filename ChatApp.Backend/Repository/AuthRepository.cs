using ChatApp.Data;
using ChatApp.Models;

namespace ChatApp.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;
        private readonly IUserRepository _userRepository;

        public AuthRepository(AppDbContext context, IUserRepository userRepository) 
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<bool> RegisterAsync(string userName, string password)
        {
            //TODO: hash the password
            User user = new User()
            {
                Name = userName,
                Password = password
            };

            if(await _userRepository.CreateUserAsync(user))
            {
                return true;
            }

            return false;
            
        }
        public async Task<bool> LoginAsync(string userName, string password)
        {
            User user = await _userRepository.GetByNameAsync(userName);

            if(user.Password == password)
            {
                return true;
            }
            else
            {
                throw new Exception("Kullanıcı adı veya şifre hatalı!");
            }

        }
    }
}
