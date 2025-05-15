using ChatApp.Models;

namespace ChatApp.Services
{
    public interface IMessageService
    {
        Task<List<Message>> GetAllMessagesAsync();
        Task<Message> GetMessageByIdAsync(int messageId);
        Task<Message> GetMessageBySenderAsync(string sender);
        Task<bool> AddMessageAsync(Message message);
        Task<bool> UpdateMessageContentAsync(int id, string content);
        Task<bool> DeleteMessageAsync(int id);
    }
}
