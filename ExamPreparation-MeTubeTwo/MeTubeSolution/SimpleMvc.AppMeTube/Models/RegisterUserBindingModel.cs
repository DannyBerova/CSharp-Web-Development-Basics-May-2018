namespace SimpleMvc.AppMeTube.Models
{
    using System.ComponentModel.DataAnnotations;
    using Infrastructure.Validations;

    public class RegisterUserBindingModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Email]
        public string Email { get; set; }
    }
}
