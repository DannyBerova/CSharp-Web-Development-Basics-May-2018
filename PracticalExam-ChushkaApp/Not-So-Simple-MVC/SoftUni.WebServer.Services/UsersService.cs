namespace SoftUni.WebServer.Services
{
    using System.Linq;
    using Common;
    using Data;
    using WebServer.Models;

    public class UsersService
    {
        private readonly ChushkaDbContext db;

        public UsersService()
        {
            this.db = new ChushkaDbContext();
        }

        public int? Create(string username, string password, string fullName, string email)
        {

            var isAdmin = !this.db.Users.Any();

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

                return user.Id;
            }
            catch
            {
                return null;
            }
        }

        public int? UserExists(string username, string password)
        {
            var userId = this.db.Users
                .FirstOrDefault(t => t.Username == username && t.PasswordHash == PasswordUtilities.GetPasswordHash(password))?.Id;

            return userId;
        }

        //public int? GetRole(string role)
        //{
        //    return this.db.Roles
        //        .FirstOrDefault
        //            (p => p.Name == role)?.Id;
        //}

        //public IEnumerable<UserWithDetailsModel> All()
        //{
        //    return this.db.Users
        //        .Include(u => u.)
        //        .Include(u => u.Posts)
        //        .Select(UserWithDetailsModel.FromPost)
        //        .ToList();
        //}

        //public string Approve(int id)
        //{
        //    var user = this.db.Users.Find(id);
        //    if (user != null && !user.IsApproved)
        //    {
        //        user.IsApproved = true;
        //        this.db.SaveChanges();
        //    }

        //    return user?.Email;
        //}
    }
}
