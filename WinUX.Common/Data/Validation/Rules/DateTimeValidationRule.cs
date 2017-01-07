namespace WinUX.Data.Validation.Rules
{
    using System;

    /// <summary>
    /// Defines a data validation rule for validating a value is a <see cref="DateTime"/>.
    /// </summary>
    public class DateTimeValidationRule : ValidationRule
    {
        /// <summary>
        ///  Gets or sets the minimum of <see cref="DateTime"/>.
        ///  The default value is DateTime.MinValue
        /// </summary>
        public DateTime MinDateTime
        {
            set;
            get;
        } = DateTime.MinValue;

        /// <summary>
        /// Gets or sets the maximum of <see cref="DateTime"/>.
        /// The default value is DateTime.MaxValue
        /// </summary>
        public DateTime MaxDateTime
        {
            set;
            get;
        } = DateTime.MaxValue;

        /// <summary>
        /// Validates the specified object is a <see cref="DateTime"/>.
        /// </summary>
        /// <param name="value">
        /// The value to validate.
        /// </param>
        /// <returns>
        /// Returns true if the object is a <see cref="DateTime"/>; else false.
        /// </returns>
        public override bool IsValid(object value)
        {
            if (value == null) return false;

            var val = value.ToString();
            if (string.IsNullOrWhiteSpace(val))
            {
                return true;
            }

            DateTime temp;
            if (DateTime.TryParse(val, out temp))
            {
                return temp >= MinDateTime && temp <= MaxDateTime;
            }
            return false;
        }
    }
}