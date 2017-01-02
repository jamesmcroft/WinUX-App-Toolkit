namespace WinUX.Data.Validation.Rules
{
    /// <summary>
    /// Defines a data validation rule for validating a value is a <see cref="decimal"/>.
    /// </summary>
    public class DecimalValidationRule : ValidationRule
    {
        /// <summary>
        /// Validates the specified object is a <see cref="decimal"/>.
        /// </summary>
        /// <param name="value">
        /// The value to validate.
        /// </param>
        /// <returns>
        /// Returns true if the object is a <see cref="decimal"/>; else false.
        /// </returns>
        public override bool IsValid(object value)
        {
            if (value == null) return false;

            var val = value.ToString();
            if (string.IsNullOrWhiteSpace(val))
            {
                return true;
            }

            decimal temp;
            return decimal.TryParse(val, out temp);
        }
    }
}