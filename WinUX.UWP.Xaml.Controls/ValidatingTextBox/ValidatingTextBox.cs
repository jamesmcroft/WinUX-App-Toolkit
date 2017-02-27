namespace WinUX.Xaml.Controls
{
    using System.Linq;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Defines a TextBox control with validation capabilities.
    /// </summary>
    [TemplatePart(Name = "ValidationText", Type = typeof(TextBlock))]
    [TemplatePart(Name = "RemainingCharacters", Type = typeof(TextBlock))]
    public partial class ValidatingTextBox : TextBox, IValidatingControl
    {
        private TextBlock remainingCharacters;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatingTextBox"/> class.
        /// </summary>
        public ValidatingTextBox()
        {
            this.DefaultStyleKey = typeof(ValidatingTextBox);

            this.ListenToProperty(
                "Visibility",
                (_, e) =>
                    {
                        if (e.NewValue != e.OldValue)
                        {
                            this.Update();
                        }
                    });
        }

        /// <summary>
        /// The validation control updated event.
        /// </summary>
        public event ValidationControlUpdatedEventHandler ValidationUpdated;

        /// <summary>
        /// Called when applying the control's template.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.remainingCharacters = this.GetTemplateChild("RemainingCharacters") as TextBlock;
            this.ValidationTextBlock = this.GetTemplateChild("ValidationText") as TextBlock;

            this.Update();

            if (!this.IsTemplateApplied)
            {
                this.TextChanged += this.OnTextChanged;
                this.IsEnabledChanged += this.OnIsEnabledChanged;

                this.ListenToProperty(nameof(this.MaxLength), (sender, args) => this.Update());

                this.IsTemplateApplied = true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the control's value meets the mandatory validation.
        /// </summary>
        /// <returns>
        /// Returns true if valid; else false.
        /// </returns>
        public bool IsMandatoryFieldValid()
        {
            if (!this.IsMandatory || !string.IsNullOrWhiteSpace(this.Text))
            {
                return true;
            }

            if (this.ValidationTextBlock != null)
            {
                this.ValidationTextBlock.Text = this.MandatoryValidationMessage;
            }

            VisualStateManager.GoToState(this, "Mandatory", true);
            return false;
        }

        /// <summary>
        /// Gets a value indicating whether the control's value is currently valid.
        /// </summary>
        /// <returns>
        /// Returns true if valid; else false.
        /// </returns>
        public bool IsValid()
        {
            this.Update();

            return !this.IsInvalid;
        }

        /// <summary>
        /// Updates the validation properties of the control.
        /// </summary>
        public void Update()
        {
            this.UpdateRemainingCharacters();

            var isInvalid = !this.IsMandatoryFieldValid();

            if (!isInvalid)
            {
                if (this.MaxLength > 0)
                {
                    isInvalid = this.Text.Length > this.MaxLength;
                }

                if (isInvalid)
                {
                    if (this.ValidationTextBlock != null)
                    {
                        this.ValidationTextBlock.Text = "The text exceeds the maximum length.";
                    }
                }
                else
                {
                    if (this.ValidationRules != null)
                    {
                        // Run through all of the validation rules for this text box and check is valid.
                        foreach (var rule in this.ValidationRules.Rules.TakeWhile(rule => !isInvalid))
                        {
                            isInvalid = !rule.IsValid(this.Text);

                            if (isInvalid && this.ValidationTextBlock != null)
                            {
                                this.ValidationTextBlock.Text = rule.ErrorMessage;
                            }
                        }
                    }
                }
            }

            this.IsInvalid = isInvalid;

            VisualStateManager.GoToState(this, this.IsInvalid ? "Invalid" : "Valid", true);

            this.ValidationUpdated?.Invoke(this);
        }

        private void UpdateRemainingCharacters()
        {
            if (this.remainingCharacters == null)
            {
                return;
            }

            if (this.MaxLength == 0)
            {
                this.remainingCharacters.Visibility = Visibility.Collapsed;
                return;
            }

            var length = 0;
            if (!string.IsNullOrEmpty(this.Text))
            {
                length = this.Text.Length;
            }
            var remainingChar = string.Format("{0}/{1}", length, this.MaxLength);

            this.remainingCharacters.Text = remainingChar;
            this.remainingCharacters.Visibility = Visibility.Visible;
        }

        private void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            this.Update();
        }

        private void OnTextChanged(object sender, TextChangedEventArgs args)
        {
            this.Update();
        }
    }
}