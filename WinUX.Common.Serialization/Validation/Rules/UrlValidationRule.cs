namespace WinUX.Data.Validation.Rules
{
    using System;

    /// <summary>
    /// Defines a data validation rule for validating a value is a URL.
    /// </summary>
    public class UrlValidationRule : ValidationRule
    {
        /// <summary>
        /// Validates the specified object against the rule.
        /// </summary>
        /// <param name="value">
        /// The value to validate.
        /// </param>
        /// <returns>
        /// Returns true if the object meets the rule's criteria; else false.
        /// </returns>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            var val = value.ToString();
            return string.IsNullOrWhiteSpace(val) || Uri.IsWellFormedUriString(val, UriKind.Absolute);
        }
    }
}