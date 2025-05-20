using ChatApp.Models;

namespace ChatApp.Data.Repository
{
    public interface IMessageRepository
    {
        Task<List<Message>> GetAll();
        Task<List<Message>> GetConversationAsync(string currentUser, string targetUser);
        Task<Message> GetById(int messageId);
        Task<Message> GetBySender(string sender);
        Task<bool> CreateMessageAsync(Message message);
        Task<bool> UpdateMessageContentAsync(int id, string content);
        Task<bool> DeleteMessageAsync(int id);
    }
}
