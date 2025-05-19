using Serilog;

namespace ChatApp.Events
{
    public class ConsoleEventPublisher : IEventPublisher
    {
        public async Task Publish(MessageSentEvent e)
        {
            Log.Information("Event Published: {@Event}", e);

            await Task.CompletedTask;
        }
    }
}
