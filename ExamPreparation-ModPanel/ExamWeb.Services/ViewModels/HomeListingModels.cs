namespace ExamWeb.Services.ViewModels
{
    using System;
    using System.Linq.Expressions;
    using ExamWeb.Models;

    public class HomeListingModels
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public static Expression<Func<Post, HomeListingModels>> FromPost =>
            k => new HomeListingModels()
            {
                Title = k.Title,
                Content = k.Content,
                CreatedBy = k.User.Email,
                CreatedOn = k.CreatedOn
            };
    }
}
