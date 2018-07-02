namespace ExamWeb.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using ViewModels;

    public class UsersService
    {
        private readonly ModPanelDbContext db;

        public UsersService()
        {
            this.db = new ModPanelDbContext();
        }

        public int? Create(string email, string password, int? position)
        {
            var isAdmin = !this.db.Users.Any();

            try
            {
                var user = new User
                {
                    Email = email,
                    PasswordHash = PasswordUtilities.GetPasswordHash(password),
                    Position = this.db.Positions.Find(position),
                    IsAdmin = isAdmin,
                    IsApproved = isAdmin
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

        public int? UserExists(string email, string password)
        {
            var userId = this.db.Users
                .FirstOrDefault(t => t.Email == email && t.PasswordHash == PasswordUtilities.GetPasswordHash(password))?.Id;

            return userId;
        }

        public int? GetPosition(string modelPosition)
        {
            return this.db.Positions
                .FirstOrDefault
                    (p => p.Name == modelPosition)?.Id;
        }

        public IEnumerable<UserWithDetailsModel> All()
        {
            return this.db.Users
                .Include(u => u.Position)
                .Include(u => u.Posts)
                .Select(UserWithDetailsModel.FromPost)
                .ToList();
        }

        public string Approve(int id)
        {
            var user = this.db.Users.Find(id);
            if (user != null && !user.IsApproved)
            {
                user.IsApproved = true;
                this.db.SaveChanges();
            }

            return user?.Email;
        }
    }
}
