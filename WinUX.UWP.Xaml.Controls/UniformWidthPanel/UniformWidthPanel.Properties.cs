namespace WinUX.Xaml.Controls
{
    using Windows.UI.Xaml;

    /// <summary>
    /// Defines the properties for the <see cref="UniformWidthPanel"/>.
    /// </summary>
    public sealed partial class UniformWidthPanel
    {
        /// <summary>
        /// Defines the dependency property for the <see cref="MaximumColumns"/>.
        /// </summary>
        public static readonly DependencyProperty MaximumColumnsProperty = DependencyProperty.Register(
            nameof(MaximumColumns),
            typeof(int),
            typeof(UniformWidthPanel),
            new PropertyMetadata(1));

        /// <summary>
        /// Gets or sets the maximum columns to show content in.
        /// </summary>
        public int MaximumColumns
        {
            get
            {
                return (int)this.GetValue(MaximumColumnsProperty);
            }
            set
            {
                this.SetValue(MaximumColumnsProperty, value);
            }
        }
    }
}