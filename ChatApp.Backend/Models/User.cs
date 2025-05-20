namespace ChatApp.Models
{
    public class User
    {
        public Guid Id { get; set; } = new Guid();
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
