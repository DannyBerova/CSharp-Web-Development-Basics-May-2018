namespace ExamWeb.App.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;
    using Infrastructure.Validations;

    public class UserRegisteringModel
    {
        //[Required]
        //[MinLength(5)]
        //public string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Email]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
       // [Regex(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}")]
        [Password]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Position { get; set; }
    }
}
