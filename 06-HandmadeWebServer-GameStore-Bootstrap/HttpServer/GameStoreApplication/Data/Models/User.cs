namespace HttpServer.GameStoreApplication.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Common;

    public class User
    {
        public int Id { get; set; }

        [Required]
        [MinLength(VaidationConstants.Account.NameMinLength)]
        [MaxLength(VaidationConstants.Account.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(VaidationConstants.Account.EmailMaxLength)]
        public string Email { get; set; }

        [Required]
        [MinLength(VaidationConstants.Account.PasswordMinLength)]
        [MaxLength(VaidationConstants.Account.PasswordMaxLength)]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public List<UserGame> Games { get; set; } = new List<UserGame>();
    }
}
