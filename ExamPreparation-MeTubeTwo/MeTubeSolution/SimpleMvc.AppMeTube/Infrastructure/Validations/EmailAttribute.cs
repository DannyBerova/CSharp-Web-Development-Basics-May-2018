namespace SimpleMvc.AppMeTube.Infrastructure.Validations
{
    using System.ComponentModel.DataAnnotations;

    public class EmailAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var email = value as string;
            if (email == null)
            {
                return true;
            }

            return email.Contains(".") && email.Contains("@");
        }
    }
}
