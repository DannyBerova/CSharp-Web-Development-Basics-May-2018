namespace KittensApp.WebIt.Services
{
    using System.Linq;
    using KittenApp.Data;
    using KittenApp.Models;
    using SimpleMvc.Common;

    public class UsersService
    {
        private readonly KittenAppContext db;

        public UsersService()
        {
            this.db = new KittenAppContext();
        }

        public User Create(string username, string password, string email)
        {
            User user = null;
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email))
            {
                return null;
            }

            try
            {
                user = new User
                {
                    Username = username,
                    PasswordHash = PasswordUtilities.GetPasswordHash(password),
                    Email = email
                };

                this.db.Users.Add(user);
                this.db.SaveChanges();

                return user;
            }
            catch
            {
                return null;
            }
        }

        public User UserExists(string username, string password)
        {
            User user = null;
            var users = this.db.Users.AsQueryable();

            user = this.db.Users.AsQueryable()
                .FirstOrDefault(t => t.Username == username && t.PasswordHash == PasswordUtilities.GetPasswordHash(password));

            return user;
        }
    }
}
