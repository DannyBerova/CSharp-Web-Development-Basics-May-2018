namespace SimpleMvc.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Note
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }
    }
}