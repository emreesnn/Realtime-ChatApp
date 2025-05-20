namespace ChatApp.Data.DTO
{
    public class MessageHistory
    {
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
