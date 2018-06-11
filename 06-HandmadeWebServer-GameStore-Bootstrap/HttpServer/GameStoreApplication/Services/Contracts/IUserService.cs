namespace HttpServer.GameStoreApplication.Services.Contracts
{
    using Data.Models;

    public interface IUserService
    {
        bool Create(string email, string name, string password);

        User FindByEmail(string email);

        bool Find(string email, string password);

        bool IsAdmin(string email);
    }
}
