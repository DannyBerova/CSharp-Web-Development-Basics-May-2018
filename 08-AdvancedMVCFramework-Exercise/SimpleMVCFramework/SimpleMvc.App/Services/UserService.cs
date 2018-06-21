namespace SimpleMvc.App.Services
{
    using BindingModels;
    using Common;
    using Data;
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public class UserService
    {
        private readonly NotesDbContext db;

        public UserService()
        {
            this.db = new NotesDbContext();
        }

        public bool Create(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            try
            {
                var user = new User
                {
                    Username = username,
                    Password = HashPassword.GetHashSha256(password)
                };

                this.db.Users.Add(user);
                this.db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UserExists(string username, string password)
            => this.db
                .Users
                .Any(u => u.Username == username && u.Password == HashPassword.GetHashSha256(password));

        public UserViewModel GetById(int id)
        {
            return this.db
                .Users
                .Where(u => u.Id == id)
                .Select(u => new UserViewModel
                {
                    Id = u.Id,
                    Username = u.Username,
                    Notes = u.Notes
                })
                .FirstOrDefault();
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
                    Notes = u.Notes
                })
                .FirstOrDefault();
        }

        public IEnumerable<UserViewModel> All()
        {
            return this.db
                .Users
                .OrderByDescending(u => u.Id)
                .Select(u => new UserViewModel
                {
                    Notes = u.Notes,
                    Username = u.Username,
                    Id = u.Id
                })
                .ToList();
        }
    }
}