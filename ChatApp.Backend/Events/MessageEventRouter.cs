
namespace ChatApp.Events
{
    public class MessageEventRouter : IMessageEventRouter
    {
        private readonly IEventPublisher _eventPublisher;

        public MessageEventRouter(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public async Task RouteAsync(MessageSentEvent e)
        {
            await _eventPublisher.Publish(e);

        }
    }
}
