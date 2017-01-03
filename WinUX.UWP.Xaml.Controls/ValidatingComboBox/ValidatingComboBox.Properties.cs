namespace WinUX.Xaml.Controls
{
    using System.Windows.Input;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Defines the properties for the <see cref="ValidatingComboBox"/>.
    /// </summary>
    public partial class ValidatingComboBox
    {
        /// <summary>
        /// Defines the dependency property for the <see cref="IsMandatory"/>.
        /// </summary>
        public static readonly DependencyProperty IsMandatoryProperty = DependencyProperty.Register(
            nameof(IsMandatory),
            typeof(bool),
            typeof(ValidatingComboBox),
            new PropertyMetadata(false, (d, e) => ((ValidatingComboBox)d).Update()));

        /// <summary>
        /// Defines the dependency property for the <see cref="MandatoryValidationMessage"/>.
        /// </summary>
        public static readonly DependencyProperty MandatoryValidationMessageProperty =
            DependencyProperty.Register(
                nameof(MandatoryValidationMessage),
                typeof(string),
                typeof(ValidatingComboBox),
                new PropertyMetadata("Required"));

        /// <summary>
        /// Defines the dependency property for the <see cref="IsInvalid"/>.
        /// </summary>
        public static readonly DependencyProperty IsInvalidProperty = DependencyProperty.Register(
            nameof(IsInvalid),
            typeof(bool),
            typeof(ValidatingComboBox),
            new PropertyMetadata(false));

        /// <summary>
        /// Defines the dependency property for the <see cref="SelectionChangedCommand"/>.
        /// </summary>
        public static readonly DependencyProperty SelectionChangedCommandProperty =
            DependencyProperty.Register(
                nameof(SelectionChangedCommand),
                typeof(ICommand),
                typeof(ValidatingComboBox),
                new PropertyMetadata(null));

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
        /// Gets or sets the command called when the selection of the <see cref="ComboBox"/> changes.
        /// </summary>
        public ICommand SelectionChangedCommand
        {
            get
            {
                return (ICommand)this.GetValue(SelectionChangedCommandProperty);
            }
            set
            {
                this.SetValue(SelectionChangedCommandProperty, value);
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