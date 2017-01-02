namespace WinUX.Data.Validation
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines a model for a collection of validation rules.
    /// </summary>
    public sealed class ValidationRules
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRules"/> class.
        /// </summary>
        public ValidationRules()
            : this(new List<ValidationRule>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRules"/> class.
        /// </summary>
        /// <param name="rules">
        /// The rules.
        /// </param>
        public ValidationRules(List<ValidationRule> rules)
        {
            this.Rules = rules;
        }

        /// <summary>
        /// Gets the rules of the collection.
        /// </summary>
        public List<ValidationRule> Rules { get; private set; }
    }
}