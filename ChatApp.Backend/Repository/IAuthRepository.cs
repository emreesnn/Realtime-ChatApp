namespace ChatApp.Repository
{
    public interface IAuthRepository
    {
        Task<bool> RegisterAsync(string userName, string password);
        Task<bool> LoginAsync(string userName, string password);

    }
}
