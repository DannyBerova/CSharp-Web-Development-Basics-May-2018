namespace SimpleMVC.Framework.Attributes.Property
{
    using System.Text.RegularExpressions;

    public class RegexAttribute : PropertyAttribute
    {
        private readonly string pattern;

        public RegexAttribute(string pattern)
        {
            this.pattern ="^" +  pattern + "$";
        }

        public override bool IsValid(object paramValue)
        {
            return Regex.IsMatch(paramValue.ToString(), this.pattern);
        }
    }
}
