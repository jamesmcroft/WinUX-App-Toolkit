namespace WinUX.Xaml.Controls
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using WinUX.Data.Validation;

    /// <summary>
    /// Defines the properties for the <see cref="ValidatingTextBox"/> control.
    /// </summary>
    public partial class ValidatingTextBox
    {
        /// <summary>
        /// Defines the dependency property for the <see cref="IsMandatory"/>.
        /// </summary>
        public static readonly DependencyProperty IsMandatoryProperty = DependencyProperty.Register(
            nameof(IsMandatory),
            typeof(bool),
            typeof(ValidatingTextBox),
            new PropertyMetadata(false, (d, e) => ((ValidatingTextBox)d).Update()));

        /// <summary>
        /// Defines the dependency property for the <see cref="ValidationRules"/>.
        /// </summary>
        public static readonly DependencyProperty ValidationRulesProperty =
            DependencyProperty.Register(
                nameof(ValidationRules),
                typeof(ValidationRules),
                typeof(ValidatingTextBox),
                new PropertyMetadata(null, (d, e) => ((ValidatingTextBox)d).Update()));

        /// <summary>
        /// Defines the dependency property for the <see cref="MandatoryValidationMessage"/>.
        /// </summary>
        public static readonly DependencyProperty MandatoryValidationMessageProperty =
            DependencyProperty.Register(
                nameof(MandatoryValidationMessage),
                typeof(string),
                typeof(ValidatingTextBox),
                new PropertyMetadata("Required"));

        /// <summary>
        /// Defines the dependency property for the <see cref="IsInvalid"/>.
        /// </summary>
        public static readonly DependencyProperty IsInvalidProperty = DependencyProperty.Register(
            nameof(IsInvalid),
            typeof(bool),
            typeof(ValidatingTextBox),
            new PropertyMetadata(false));

        /// <summary>
        /// Gets or sets the validation rules to run against the control's value.
        /// </summary>
        public ValidationRules ValidationRules
        {
            get
            {
                return (ValidationRules)this.GetValue(ValidationRulesProperty);
            }
            set
            {
                this.SetValue(ValidationRulesProperty, value);
            }
        }

        /// <summary>
        /// Gets or set a value indicating whether the value is required by the control to be valid.
        /// </summary>
        public bool IsMandatory
        {
            get
            {
                return (bool)this.GetValue(IsMandatoryProperty);
            }
            set
            {
                this.SetValue(IsMandatoryProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the message to display when the value is required.
        /// </summary>
        public string MandatoryValidationMessage
        {
            get
            {
                return (string)this.GetValue(MandatoryValidationMessageProperty);
            }
            set
            {
                this.SetValue(MandatoryValidationMessageProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the value is invalid.
        /// </summary>
        public bool IsInvalid
        {
            get
            {
                return (bool)this.GetValue(IsInvalidProperty);
            }
            set
            {
                this.SetValue(IsInvalidProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="TextBlock"/> used to show the validation messages.
        /// </summary>
        public TextBlock ValidationTextBlock { get; set; }

        /// <summary>
        /// Gets a value indicating whether the control's template has been applied.
        /// </summary>
        public bool IsTemplateApplied { get; private set; }
    }
}