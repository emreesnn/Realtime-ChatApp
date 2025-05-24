namespace ChatApp.Models
{
    public class User
    {
        public Guid Id { get; set; } = new Guid();
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsOnline { get; set; } = false;
    }
}
