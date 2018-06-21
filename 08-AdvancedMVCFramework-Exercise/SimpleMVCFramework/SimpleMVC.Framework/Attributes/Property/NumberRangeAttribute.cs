namespace SimpleMVC.Framework.Attributes.Property
{

    public class NumberRangeAttribute : PropertyAttribute
    {
        private readonly double minimumValue;
        private readonly double maximumValue;

        public NumberRangeAttribute(double minimumValue, double maximumValue)
        {
            this.minimumValue = minimumValue;
            this.maximumValue = maximumValue;
        }

        public override bool IsValid(object paramValue)
        {
            return this.minimumValue <= (double) paramValue && this.maximumValue >= (double) paramValue;
        }
    }
}
