using ChatApp.Models;
using ChatApp.Repository;
using Microsoft.AspNetCore.SignalR;
using Serilog;
using System.Reflection;

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
            Users.TryAdd(Context.ConnectionId, userId);

            User user = await _userRepository.GetByIdAsync(userId);
            user.IsOnline = true;
            await _userRepository.UpdateUserAsync(user, userId);

            await Clients.All.SendAsync("Users", user);

            var allOnlineUsers = await _userRepository.GetAllAsync();
            var othersOnline = allOnlineUsers
                .Where(u => u.Id != user.Id && u.IsOnline)
                .ToList();

            foreach (var other in othersOnline)
            {
                await Clients.Caller.SendAsync("Users", other);
            }
        }


        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Guid userId;
            Users.TryGetValue(Context.ConnectionId, out userId);
            Users.Remove(Context.ConnectionId);

            User? user = await _userRepository.GetByIdAsync(userId);
            user.IsOnline = false;
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
                ReceiverId = targetUserId,
                ReceiverName = targetUser.Name,
                Content = message,
                Timestamp = DateTime.UtcNow
            });

            var senderConnectionId = Users.FirstOrDefault(x => x.Value == currentUserId).Key;
            var receiverConnectionId = Users.FirstOrDefault(x => x.Value == targetUserId).Key;

            await Clients.Clients(senderConnectionId, receiverConnectionId)
                .SendAsync("ReceiveMessage", currentUser.Name, message);

        }


    }
}
