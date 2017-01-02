namespace WinUX.Data.Validation.Rules
{
    /// <summary>
    /// Defines a data validation rule for validating a value is an e-mail address.
    /// </summary>
    public class EmailValidationRule : RegexValidationRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailValidationRule"/> class.
        /// </summary>
        public EmailValidationRule()
        {
            this.Regex = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                         + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
        }
    }
}