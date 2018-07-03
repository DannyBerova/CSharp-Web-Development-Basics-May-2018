namespace SoftUni.WebServer.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public int RoleId { get; set; }
        [Required]
        public Role Role { get; set; }

        public IList<Order> Products = new List<Order>();
    }
}
