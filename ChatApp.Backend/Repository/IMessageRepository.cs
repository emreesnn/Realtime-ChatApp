using ChatApp.Models;

namespace ChatApp.Repository
{
    public interface IMessageRepository
    {
        Task<List<Message>> GetAll();
        Task<List<Message>> GetConversationAsync(string currentUserName, string targetUserName);
        Task<bool> CreateMessageAsync(Message message);
        Task<bool> UpdateMessageContentAsync(int id, string content);
        Task<bool> DeleteMessageAsync(int id);
    }
}
