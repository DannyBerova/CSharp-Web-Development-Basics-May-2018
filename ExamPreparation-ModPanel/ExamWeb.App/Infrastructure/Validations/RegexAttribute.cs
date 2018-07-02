namespace ExamWeb.App.Infrastructure.Validations
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;

    public class RegexAttribute : ValidationAttribute
    {
        private readonly string pattern;

        public RegexAttribute(string pattern)
        {
            this.pattern = "^" + pattern + "$";
            this.ErrorMessage =
                "Password must contain at least one Capital letter, one small letter and one digit to mach the pattern!";
        }

        public override bool IsValid(object value) =>
            Regex.IsMatch(value.ToString(), this.pattern);

        
    }
}
