namespace WinUX.Xaml.Behaviors.TextBox
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using Microsoft.Xaml.Interactivity;

    /// <summary>
    /// Defines a behavior that will update a Text binding as the text is entered into the TextBox control.
    /// </summary>
    public sealed class TextBoxTextUpdateBehavior : Behavior
    {
        /// <summary>
        /// Defines the dependency property for <see cref="Text"/>.
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(TextBoxTextUpdateBehavior),
            new PropertyMetadata(
                string.Empty,
                (d, e) => ((TextBoxTextUpdateBehavior)d).SetTextBoxText((string)e.NewValue)));

        /// <summary>
        /// Gets or sets the text of the associated <see cref="TextBox"/>.
        /// </summary>
        public string Text
        {
            get
            {
                return (string)this.GetValue(TextProperty);
            }
            set
            {
                this.SetValue(TextProperty, value);
            }
        }

        private TextBox TextBox => this.AssociatedObject as TextBox;

        /// <summary>
        /// Called after the behavior is attached to the <see cref="P:Microsoft.Xaml.Interactivity.Behavior.AssociatedObject" />.
        /// </summary>
        protected override void OnAttached()
        {
            if (this.TextBox != null)
            {
                this.TextBox.TextChanged += this.TextBox_OnTextChanged;
            }
        }

        /// <summary>
        /// Called when the behavior is being detached from its <see cref="P:Microsoft.Xaml.Interactivity.Behavior.AssociatedObject" />.
        /// </summary>
        protected override void OnDetaching()
        {
            if (this.TextBox != null)
            {
                this.TextBox.TextChanged -= this.TextBox_OnTextChanged;
            }
        }

        private void TextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.TextBox != null)
            {
                this.Text = this.TextBox.Text;
            }
        }

        private void SetTextBoxText(string text)
        {
            if (this.TextBox != null)
            {
                this.TextBox.Text = text;
            }
        }
    }
}