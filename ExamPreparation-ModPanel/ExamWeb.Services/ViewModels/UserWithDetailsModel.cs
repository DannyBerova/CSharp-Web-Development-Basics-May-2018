namespace ExamWeb.Services.ViewModels
{
    using System;
    using System.Linq.Expressions;
    using ExamWeb.Models;

    public class UserWithDetailsModel
    {
        public int Id { get; set; }

        //public string Username { get; set; }

        public string Email { get; set; }

        public string Position { get; set; }

        public bool IsApproved { get; set; }

        public int Posts { get; set; }

        public static Expression<Func<User, UserWithDetailsModel>> FromPost =>
            k => new UserWithDetailsModel()
            {
                Id = k.Id,
                Email = k.Email,
                Position = k.Position.Name,
                IsApproved = k.IsApproved,
                Posts = k.Posts.Count
            };
    }
}
