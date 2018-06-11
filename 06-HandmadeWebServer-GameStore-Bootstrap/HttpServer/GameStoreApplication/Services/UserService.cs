namespace HttpServer.GameStoreApplication.Services
{
    using System.Linq;

    using Contracts;
    using Data.Models;
    using Data;

    public class UserService : IUserService
    {
        public bool Create(string email, string name, string password)
        {
            using (var db = new GameStoreDbContext())
            {
                if (db.Users.Any(u => u.Email == email))
                {
                    return false;
                }

                var isAdmin = !db.Users.Any();

                var user = new User()
                {
                    Email = email,
                    Name = name,
                    Password = password,
                    IsAdmin = isAdmin
                };

                db.Add(user);
                db.SaveChanges();
            }

            return true;
        }

        public User FindByEmail(string email)
        {
            using (var db = new GameStoreDbContext())
            {
                return db.Users.FirstOrDefault(u => u.Email == email);
            }
        }

        public bool Find(string email, string password)
        {
            using (var db = new GameStoreDbContext())
            {
                return db.Users.Any(u => u.Email == email && u.Password == password);
            }
        }

        public bool IsAdmin(string email)
        {
            using (var db = new GameStoreDbContext())
            {
                return db.Users.Any(u => u.Email == email && u.IsAdmin);
            }
        }
    }
}
