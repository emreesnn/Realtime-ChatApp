using ChatApp.Data;
using ChatApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ChatApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(Guid userId)
        {
            User? user = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new BadHttpRequestException("Kullanıcı bulunamadı!");
            }
            return user;
        }
        
        public async Task<User> GetByNameAsync(string userName)
        {
            User? user = await _context.Users.Where(u => u.Name == userName).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new BadHttpRequestException("Kullanıcı bulunamadı!");
            }
            return user;
        }
        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }
        
        public async Task<bool> CreateUserAsync(User user)
        {
            if (user == null)
            {
                throw new BadHttpRequestException("Kullanıcı bilgileri eksik!");
            }

            bool isNameExists = await _context.Users.AnyAsync(u => u.Name == user.Name);

            if (isNameExists)
            {
                throw new BadHttpRequestException("Kullanıcı adı kullanılıyor!");
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateUserAsync(User updatedUser, Guid userId)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new BadHttpRequestException("Kullanıcı bulunamadı!");
            }

            //user.Name = updatedUser.Name;
            user.Password = updatedUser.Password;
            user.IsOnline = updatedUser.IsOnline;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            User? user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new BadHttpRequestException("Kullanıcı bulunamadı!");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }



    }
}
