namespace SoftUni.WebServer.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Role
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public IList<User> Users { get; set; } = new List<User>();
    }
}