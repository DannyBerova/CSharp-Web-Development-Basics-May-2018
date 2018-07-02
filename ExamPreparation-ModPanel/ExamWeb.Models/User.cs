namespace ExamWeb.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public int Id { get; set; }

        //[Required]
        //[MinLength(5)]
        //public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public int PositionId { get; set; }

        public Position Position { get; set; }

        public bool IsApproved { get; set; }

        public bool IsAdmin { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>();


    }
}
