namespace SimpleMvc.Services
{
    using System.Linq;
    using Common;
    using Data;
    using Modelss;
    using ViewModels;

    public class UsersService
    {
        private readonly MeTubeDbContext db;

        public UsersService()
        {
            this.db = new MeTubeDbContext();
        }

        public int? Create(string username, string password, string email)
        {
            try
            {
                var user = new User
                {
                    Username = username,
                    Password = PasswordUtilities.GetPasswordHash(password),
                    Email = email
                };

                this.db.Users.Add(user);
                this.db.SaveChanges();

                return user.Id;
            }
            catch
            {
                return null;
            }
        }

        public int? UserExists(string username, string password)
        {
            var userIds = this.db.Users.AsQueryable();

            var Id = userIds
                .FirstOrDefault(t => t.Username == username && t.Password == PasswordUtilities.GetPasswordHash(password))?.Id;

            return Id;
        }

        public UserViewModel GetByName(string username)
        {
            return this.db
                .Users
                .Where(u => u.Username == username)
                .Select(u => new UserViewModel
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    Tubes = u.Tubes
                })
                .FirstOrDefault();
        }
    }

}

