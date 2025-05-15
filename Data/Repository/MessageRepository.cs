using ChatApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Data.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext _context;

        public MessageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Message>> GetAll()
        {
            return await _context.Message.ToListAsync();
        }

        public async Task<Message> GetById(int messageId)
        {
            return await _context.Message
                .Where(m => m.Id == messageId)
                .FirstOrDefaultAsync();
        }

        public async Task<Message> GetBySender(string sender)
        {
            return await _context.Message
                .Where(m => m.Sender == sender)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CreateMessageAsync(Message message)
        {
            _context.Message.Add(message);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateMessageContentAsync(int id, string content)
        {
            var message = await _context.Message.FindAsync(id);
            if (message == null) return false;

            message.Content = content;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMessageAsync(int id)
        {
            var message = await _context.Message.FindAsync(id);
            if (message == null) return false;

            _context.Message.Remove(message);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
