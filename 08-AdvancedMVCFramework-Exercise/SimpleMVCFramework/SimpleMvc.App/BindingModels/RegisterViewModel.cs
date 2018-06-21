namespace SimpleMvc.App.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Password { get; set; }

        [Required]
        //[Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}