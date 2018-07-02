namespace ExamWeb.Services.ViewModels
{
    using System;
    using System.Linq.Expressions;
    using Models;

    public class PostListingModels
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public static Expression<Func<Post, PostListingModels>> FromPost =>
            p => new PostListingModels()
            {
                Id = p.Id,
                Title = p.Title
            };
    }
}
