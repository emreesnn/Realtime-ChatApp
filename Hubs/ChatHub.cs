using ChatApp.Data.Repository;
using ChatApp.Events;
using ChatApp.Models;
using Microsoft.AspNetCore.SignalR;
using Serilog;

namespace ChatApp.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMessageEventRouter _messageEventRouter;

        public ChatHub(IMessageRepository messageRepository, IMessageEventRouter messageEventRouter)
        {
            _messageRepository = messageRepository;
            _messageEventRouter = messageEventRouter;
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage",user, message);

            await _messageRepository.CreateMessageAsync(
                new Message
                {
                    Content = message,
                    Sender = user
                }
            );

            MessageSentEvent messageSentEvent = new MessageSentEvent
            {
                Content = message
            };

            await _messageEventRouter.RouteAsync(messageSentEvent);

        }
    }
}
