namespace SimpleMvc.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Note
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Content { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        public bool IsFinished { get; set; } = false;

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }
    }
}