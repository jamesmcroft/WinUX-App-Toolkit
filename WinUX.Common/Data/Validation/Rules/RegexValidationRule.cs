namespace WinUX.Data.Validation.Rules
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// Defines a data validation rule for validating a value matches a regular expression.
    /// </summary>
    public class RegexValidationRule : ValidationRule
    {
        /// <summary>
        /// Gets or sets the regular expression to match against.
        /// </summary>
        public string Regex { get; set; }

        /// <summary>
        /// Validates the specified object matches the <see cref="Regex"/>.
        /// </summary>
        /// <param name="value">
        /// The value to validate.
        /// </param>
        /// <returns>
        /// Returns true if the object matches; else false.
        /// </returns>
        public override bool IsValid(object value)
        {
            if (value == null) return false;

            var val = value.ToString();
            if (string.IsNullOrWhiteSpace(val))
            {
                return true;
            }

            var reg = new Regex(this.Regex, RegexOptions.IgnoreCase);
            return reg.IsMatch(val);
        }
    }
}