namespace ExamWeb.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Post
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MinLength(3)]
        public string Content { get; set; }

        public DateTime? CreatedOn { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
