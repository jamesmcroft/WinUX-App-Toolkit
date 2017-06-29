namespace WinUX.Data.Validation.Rules
{
    /// <summary>
    /// Defines a data validation rule for validating a value is a <see cref="double"/>.
    /// </summary>
    public class DoubleValidationRule : ValidationRule
    {
        /// <summary>
        /// Validates the specified object is a <see cref="double"/>.
        /// </summary>
        /// <param name="value">
        /// The value to validate.
        /// </param>
        /// <returns>
        /// Returns true if the object is a <see cref="double"/>; else false.
        /// </returns>
        public override bool IsValid(object value)
        {
            if (value == null) return false;

            var val = value.ToString();
            if (string.IsNullOrWhiteSpace(val))
            {
                return true;
            }

            double temp;
            return double.TryParse(val, out temp);
        }
    }
}