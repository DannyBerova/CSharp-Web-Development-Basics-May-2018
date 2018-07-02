namespace SimpleMvc.Framework.Attributes.Property
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

            return password.Any(char.IsDigit)
                   && password.Any(char.IsUpper)
                   && password.Any(char.IsLower)
                   && password.Length >= 6;
        }
    }
}
