namespace ChatApp.Events
{
    public interface IMessageEventRouter
    {
        Task RouteAsync(MessageSentEvent e);
    }
}
