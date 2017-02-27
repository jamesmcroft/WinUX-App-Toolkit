namespace WinUX.Xaml.Controls
{
    using System.Linq;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Defines a ComboBox control with validation capabilities.
    /// </summary>
    [TemplatePart(Name = "ValidationText", Type = typeof(TextBlock))]
    public partial class ValidatingComboBox : ComboBox, IValidatingControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatingComboBox"/> class.
        /// </summary>
        public ValidatingComboBox()
        {
            this.DefaultStyleKey = typeof(ValidatingComboBox);

            this.ListenToProperty(
                "Visibility",
                (d, e) =>
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

            this.ValidationTextBlock = this.GetTemplateChild("ValidationText") as TextBlock;

            this.Update();

            if (!this.IsTemplateApplied)
            {
                this.SelectionChanged += this.OnSelectionChanged;
                this.IsEnabledChanged += this.OnIsEnabledChanged;

                this.IsTemplateApplied = true;
            }
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Update();

            if (this.SelectionChangedCommand == null) return;

            var selectedItem = e.AddedItems.FirstOrDefault();

            if (this.SelectionChangedCommand.CanExecute(selectedItem))
            {
                this.SelectionChangedCommand.Execute(selectedItem);
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
            if (!this.IsMandatory || !string.IsNullOrWhiteSpace(this.SelectedValue?.ToString())) return true;

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
            var isInvalid = !this.IsMandatoryFieldValid();

            this.IsInvalid = isInvalid;

            VisualStateManager.GoToState(this, this.IsInvalid ? "Invalid" : "Valid", true);

            this.ValidationUpdated?.Invoke(this);
        }

        private void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            this.Update();
        }
    }
}