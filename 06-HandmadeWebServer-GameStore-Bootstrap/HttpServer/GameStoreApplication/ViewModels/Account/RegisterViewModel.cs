namespace HttpServer.GameStoreApplication.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;

    using Common;
    using Utilities;

    public class RegisterViewModel
    {
        [Display(Name = "E-mail")]
        [Required]
        [MaxLength(VaidationConstants.Account.EmailMaxLength,
            ErrorMessage = VaidationConstants.IvalidMaxLegnthErrorMessage)]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Full Name")]
        [MinLength(VaidationConstants.Account.NameMinLength,
            ErrorMessage = VaidationConstants.IvalidMinLegnthErrorMessage)]
        [MaxLength(VaidationConstants.Account.NameMaxLength,
            ErrorMessage = VaidationConstants.IvalidMaxLegnthErrorMessage)]
        public string FullName { get; set; }

        [Required]
        [MinLength(VaidationConstants.Account.PasswordMinLength,
            ErrorMessage = VaidationConstants.IvalidMinLegnthErrorMessage)]
        [MaxLength(VaidationConstants.Account.PasswordMaxLength,
            ErrorMessage = VaidationConstants.IvalidMaxLegnthErrorMessage)]
        [Password]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
