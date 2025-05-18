namespace ChatApp.Events
{
    public class MessageSentEvent
    {
        public string Sender { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
