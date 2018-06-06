namespace MyWebServer.ByTheCakeApp.Services
{
    using Contracts;
    using Data;
    using Data.Models;
    using System;
    using System.Linq;
    using UTilities;
    using ViewModels.Account;

    public class UserService : IUserService
    {
        public bool Create(string name, string username, string password)
        {
            using (var context = new ByTheCakeDbContext())
            {
                if (context.Users.Any(u => u.Username == username))
                {
                    return false;
                }

                var user = new User
                {
                    Name = name,
                    Username = username,
                    Password = PasswordUtilities.ComputeHash(password),
                    RegistrationDate = DateTime.UtcNow
                };

                context.Add(user);
                context.SaveChanges();

                return true;
            }
        }

        public bool Find(string username, string password)
        {
            using (var context = new ByTheCakeDbContext())
            {
                return context
                    .Users
                    .Any(u => u.Username == username &&
                              u.Password == PasswordUtilities.ComputeHash(password));
            }
        }

        public ProfileViewModel Profile(string username)
        {
            using (var context = new ByTheCakeDbContext())
            {
                return context
                    .Users
                    .Where(u => u.Username == username)
                    .Select(u => new ProfileViewModel
                    {
                        Name = u.Name,
                        RegistrationDate = u.RegistrationDate,
                        TotalOrders = u.Orders.Count
                    })
                    .FirstOrDefault();
            }
        }

        public int? GetUserId(string username)
        {
            using (var context = new ByTheCakeDbContext())
            {
                var id = context
                    .Users
                    .Where(u => u.Username == username)
                    .Select(u => u.Id)
                    .FirstOrDefault();

                return id != 0 ? (int?)id : null;
            }
        }
    }
}
