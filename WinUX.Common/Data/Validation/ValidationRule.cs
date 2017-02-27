namespace WinUX.Data.Validation
{
    /// <summary>
    /// Defines a base model for data validation rules.
    /// </summary>
    public abstract class ValidationRule
    {
        public static string DefaultErrorMessage = "Field invalid.";

        private string errorMessage;

        /// <summary>
        /// Gets the error message to display for the rule.
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.errorMessage) ? DefaultErrorMessage : this.errorMessage;
            }
            set
            {
                this.errorMessage = value;
            }
        }

        /// <summary>
        /// Validates the specified object against the rule.
        /// </summary>
        /// <param name="value">
        /// The value to validate.
        /// </param>
        /// <returns>
        /// Returns true if the object meets the rule's criteria; else false.
        /// </returns>
        public abstract bool IsValid(object value);
    }
}