namespace SimpleMvc.App.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Models;
    using SimpleMvc.Models;

    public class UserService
    {
        private readonly NotesDbContext db;

        public UserService()
        {
            this.db = new NotesDbContext();
        }

        public bool Register(string username, string password)
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
                    Password = password
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

        public IEnumerable<UserViewModel> All()
        {
            return this.db
                .Users
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