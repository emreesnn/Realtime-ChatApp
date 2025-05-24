using ChatApp.Data;
using ChatApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Repository
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
            return await _context.Messages.ToListAsync();
        }
        public async Task<List<Message>> GetConversationAsync(string currentUserName, string targetUserName)
        {
            if (string.IsNullOrEmpty(currentUserName) || string.IsNullOrEmpty(targetUserName))
            {
                throw new BadHttpRequestException("Kullanıcı adı bulunamadı!");
            }
            return await _context.Messages
                .Where(m =>
                   m.SenderName == currentUserName && m.ReceiverName == targetUserName ||
                   m.SenderName == targetUserName && m.SenderName == currentUserName)
                .OrderBy(m => m.Timestamp)
                .ToListAsync();
        }

        public async Task<bool> CreateMessageAsync(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateMessageContentAsync(int id, string content)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null) return false;

            message.Content = content;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMessageAsync(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null) return false;

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
