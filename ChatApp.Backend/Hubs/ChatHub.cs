using ChatApp.Models;
using ChatApp.Repository;
using Microsoft.AspNetCore.SignalR;
using Serilog;

namespace ChatApp.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;

        public ChatHub(IMessageRepository messageRepository, IUserRepository userRepository)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }

        public static Dictionary<string, Guid> Users = new();
        public async Task Connect(Guid userId)
        {
            Users.Add(Context.ConnectionId, userId);
            User? user = await _userRepository.GetByIdAsync(userId);
            user.IsOnline = true;
            await _userRepository.UpdateUserAsync(user, userId);

            await Clients.All.SendAsync("Users", user);
            
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Guid userId;
            Users.TryGetValue(Context.ConnectionId, out userId);
            Users.Remove(Context.ConnectionId);

            User? user = await _userRepository.GetByIdAsync(userId);
            user.IsOnline = true;
            await _userRepository.UpdateUserAsync(user, userId);

            await Clients.All.SendAsync("Users", user);
        }

        public async Task SendPrivateMessage(string userId, string targetId, string message)
        {

            Guid currentUserId = Guid.Parse(userId);
            Guid targetUserId = Guid.Parse(targetId);
            
            User currentUser = await _userRepository.GetByIdAsync(currentUserId);
            User targetUser = await _userRepository.GetByIdAsync(targetUserId);

            await _messageRepository.CreateMessageAsync(new Message
            {
                SenderId = currentUserId,
                SenderName = currentUser.Name,
                ReceiverId = currentUserId,
                ReceiverName = targetUser.Name,
                Content = message,
                Timestamp = DateTime.UtcNow
            });

            await Clients.User(userId).SendAsync("ReceiveMessage",targetId, message);

        }


    }
}
