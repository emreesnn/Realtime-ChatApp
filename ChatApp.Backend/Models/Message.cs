namespace ChatApp.Models
{
    public class Message
    {
        public int Id { get; set; }

        public Guid SenderId { get; set; }
        public string SenderName { get; set; } = string.Empty;

        public Guid ReceiverId { get; set; }
        public string ReceiverName { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

}
