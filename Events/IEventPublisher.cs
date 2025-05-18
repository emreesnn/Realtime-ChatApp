namespace ChatApp.Events
{
    public interface IEventPublisher
    {
        Task Publish(MessageSentEvent e);
    }
}
