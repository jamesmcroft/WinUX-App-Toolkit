namespace WinUX.Xaml.Controls
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Defines a control for providing a header for read-only text.
    /// </summary>
    [TemplatePart(Name = "HeaderContent", Type = typeof(TextBlock))]
    public partial class HeaderedTextBlock : Control
    {
        private TextBlock headerContent;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderedTextBlock"/> class.
        /// </summary>
        public HeaderedTextBlock()
        {
            this.DefaultStyleKey = typeof(HeaderedTextBlock);
        }

        /// <summary>
        /// Called when applying the control's template.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.headerContent = this.GetTemplateChild("HeaderContent") as TextBlock;

            this.UpdateVisibility();
        }

        private void UpdateHeader()
        {
            if (this.headerContent != null)
            {
                this.UpdateVisibility();
            }
        }

        private void UpdateVisibility()
        {
            if (this.headerContent != null)
            {
                this.headerContent.Visibility = string.IsNullOrWhiteSpace(this.headerContent.Text)
                                                    ? Visibility.Collapsed
                                                    : Visibility.Visible;
            }
        }

        private void UpdateForOrientation(Orientation orientationValue)
        {
            switch (orientationValue)
            {
                case Orientation.Vertical:
                    VisualStateManager.GoToState(this, "Vertical", true);
                    break;
                case Orientation.Horizontal:
                    VisualStateManager.GoToState(this, "Horizontal", true);
                    break;
            }
        }
    }
}