namespace ExamWeb.Services.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    using Models;

    public class PostModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MinLength(3)]
        public string Content { get; set; }

        public static Expression<Func<Post, PostModel>> FromPost =>
            p => new PostModel()
            {
                Title = p.Title,
                Content = p.Content
            };
    }
}
