namespace WinUX.Xaml.Controls
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Defines the properties for the <see cref="DraggableContentControl"/>.
    /// </summary>
    public sealed partial class DraggableContentControl
    {
        /// <summary>
        /// Defines the dependency property for the <see cref="IsScalingEnabled"/>.
        /// </summary>
        public static readonly DependencyProperty IsScalingEnabledProperty =
            DependencyProperty.Register(
                nameof(IsScalingEnabled),
                typeof(bool),
                typeof(DraggableContentControl),
                new PropertyMetadata(false));

        /// <summary>
        /// Defines the dependency property for the <see cref="IsRotatingEnabled"/>.
        /// </summary>
        public static readonly DependencyProperty IsRotatingEnabledProperty =
            DependencyProperty.Register(
                nameof(IsRotatingEnabled),
                typeof(bool),
                typeof(DraggableContentControl),
                new PropertyMetadata(false));

        /// <summary>
        /// Gets or sets a value indicating whether scaling is enabled.
        /// </summary>
        public bool IsScalingEnabled
        {
            get
            {
                return (bool)this.GetValue(IsScalingEnabledProperty);
            }
            set
            {
                this.SetValue(IsScalingEnabledProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether rotating is enabled.
        /// </summary>
        public bool IsRotatingEnabled
        {
            get
            {
                return (bool)this.GetValue(IsRotatingEnabledProperty);
            }
            set
            {
                this.SetValue(IsRotatingEnabledProperty, value);
            }
        }

        private Grid ManipulationGrid { get; set; }

        private ContentPresenter ContentPart { get; set; }
    }
}