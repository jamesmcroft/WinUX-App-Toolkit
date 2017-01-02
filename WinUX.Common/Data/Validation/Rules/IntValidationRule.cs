namespace WinUX.Data.Validation.Rules
{
    /// <summary>
    /// Defines a data validation rule for validating a value is an <see cref="int"/>.
    /// </summary>
    public class IntValidationRule : ValidationRule
    {
        /// <summary>
        /// Validates the specified object is an <see cref="int"/>.
        /// </summary>
        /// <param name="value">
        /// The value to validate.
        /// </param>
        /// <returns>
        /// Returns true if the object is am <see cref="int"/>; else false.
        /// </returns>
        public override bool IsValid(object value)
        {
            if (value == null) return false;

            var val = value.ToString();
            if (string.IsNullOrWhiteSpace(val))
            {
                return true;
            }

            int temp;
            return int.TryParse(val, out temp);
        }
    }
}