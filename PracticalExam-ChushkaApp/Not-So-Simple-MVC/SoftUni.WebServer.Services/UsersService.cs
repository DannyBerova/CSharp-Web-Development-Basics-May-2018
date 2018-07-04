namespace SoftUni.WebServer.Services
{
    using System.Linq;
    using Common;
    using Data;
    using Models;
    using WebServer.Models;

    public class UsersService
    {
        private readonly ChushkaDbContext db;

        public UsersService()
        {
            this.db = new ChushkaDbContext();
        }

        public UserAfterCreationModel Create(string username, string password, string fullName, string email)
        {

            var isAdmin = !this.db.Users.Any();

            UserAfterCreationModel modelUser = null;
            try
            {
                var user = new User
                {
                    Username = username,
                    PasswordHash = PasswordUtilities.GetPasswordHash(password),
                    Role = isAdmin ? this.db.Roles.Find(1): this.db.Roles.Find(2),
                    FullName = fullName,
                    Email = email
                };

                this.db.Users.Add(user);
                this.db.SaveChanges();

                modelUser = new UserAfterCreationModel()
                {
                    Id = user.Id,
                    RoleId = user.RoleId,
                    RoleName = user.Role.Name
                };
            }
            catch
            {
                return modelUser;
            }

            return null;
        }

        public UserAfterCreationModel UserExists(string username, string password)
        {
            var userId = this.db.Users
                .Where(t => t.Username == username && t.PasswordHash == PasswordUtilities.GetPasswordHash(password))
                .Select(UserAfterCreationModel.FromUser)
                .FirstOrDefault();

            return userId;
        }

        public int? GetRole(string role)
        {
            return this.db.Roles
                .FirstOrDefault
                    (p => p.Name == role)?.Id;
        }    
    }
}
