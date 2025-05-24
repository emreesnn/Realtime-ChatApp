using ChatApp.Models;

namespace ChatApp.Services
{
    public interface IMessageService
    {
        Task<List<Message>> GetAllMessagesAsync();
        Task<List<Message>> GetConversationAsync(string currentUser, string targetUser);
        Task<bool> AddMessageAsync(Message message);
        Task<bool> UpdateMessageContentAsync(int id, string content);
        Task<bool> DeleteMessageAsync(int id);
    }
}
