using ChatApp.Models;
using ChatApp.Repository;

namespace ChatApp.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<List<Message>> GetAllMessagesAsync()
        {
            return await _messageRepository.GetAll();
        }
        public async Task<List<Message>> GetConversationAsync(string currentUser, string targetUser)
        {
            return await _messageRepository.GetConversationAsync(currentUser, targetUser);
        }

        public async Task<bool> AddMessageAsync(Message message)
        {
            return await _messageRepository.CreateMessageAsync(message);
        }

        public async Task<bool> UpdateMessageContentAsync(int id, string content)
        {
            return await _messageRepository.UpdateMessageContentAsync(id, content);
        }

        public async Task<bool> DeleteMessageAsync(int id)
        {
            return await _messageRepository.DeleteMessageAsync(id);
        }
    }
}
