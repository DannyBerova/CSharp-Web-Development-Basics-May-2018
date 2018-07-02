namespace SimpleMvc.AppMeTube.Infrastructure.Validations
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class PasswordAttribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            var password = value as string;
            if (password == null)
            {
                return true;
            }

            this.ErrorMessage = "Password must contain at least one Capital latin letter, one small latin letter and one digit to mach the pattern!";

            return password.Any(char.IsDigit)
                   && password.Any(char.IsUpper)
                   && password.Any(char.IsLower)
                   && password.Length >= 6;
        }
    }
}
