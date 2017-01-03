namespace WinUX.Xaml.Controls
{
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Defines an interface for a validating control.
    /// </summary>
    public interface IValidatingControl
    {
        /// <summary>
        /// The validation control updated event.
        /// </summary>
        event ValidationControlUpdatedEventHandler ValidationUpdated;

        /// <summary>
        /// Gets or set a value indicating whether the value is required by the control to be valid.
        /// </summary>
        bool IsMandatory { get; set; }

        /// <summary>
        /// Gets or sets the message to display when the value is required.
        /// </summary>
        string MandatoryValidationMessage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the value is invalid.
        /// </summary>
        bool IsInvalid { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="TextBlock"/> used to show the validation messages.
        /// </summary>
        TextBlock ValidationTextBlock { get; set; }

        /// <summary>
        /// Gets a value indicating whether the control's template has been applied.
        /// </summary>
        bool IsTemplateApplied { get; }

        /// <summary>
        /// Gets a value indicating whether the control's value meets the mandatory validation.
        /// </summary>
        /// <returns>
        /// Returns true if valid; else false.
        /// </returns>
        bool IsMandatoryFieldValid();

        /// <summary>
        /// Gets a value indicating whether the control's value is currently valid.
        /// </summary>
        /// <returns>
        /// Returns true if valid; else false.
        /// </returns>
        bool IsValid();

        /// <summary>
        /// Updates the validation properties of the control.
        /// </summary>
        void Update();
    }
}