namespace WinUX.Xaml.Controls
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Defines the properties for the <see cref="WrapPanel"/>.
    /// </summary>
    public sealed partial class WrapPanel
    {
        /// <summary>
        /// Defines the dependency property for the <see cref="Orientation"/>.
        /// </summary>
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
            nameof(Orientation),
            typeof(Orientation),
            typeof(WrapPanel),
            new PropertyMetadata(Orientation.Horizontal, (d, e) => ((WrapPanel)d).OnOrientationChanged(e)));

        /// <summary>
        /// Defines the dependency property for the <see cref="ItemHeight"/>.
        /// </summary>
        public static readonly DependencyProperty ItemHeightProperty = DependencyProperty.Register(
            nameof(ItemHeight),
            typeof(double),
            typeof(WrapPanel),
            new PropertyMetadata(double.NaN, (d, e) => ((WrapPanel)d).OnItemSizeChanged(e)));

        /// <summary>
        /// Defines the dependency property for the <see cref="ItemWidth"/>.
        /// </summary>
        public static readonly DependencyProperty ItemWidthProperty = DependencyProperty.Register(
            nameof(ItemWidth),
            typeof(double),
            typeof(WrapPanel),
            new PropertyMetadata(double.NaN, (d, e) => ((WrapPanel)d).OnItemSizeChanged(e)));

        /// <summary>
        /// Gets or sets the orientation of the panel.
        /// </summary>
        public Orientation Orientation
        {
            get
            {
                return (Orientation)this.GetValue(OrientationProperty);
            }
            set
            {
                this.SetValue(OrientationProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the height of the layout area for each item.
        /// </summary>
        public double ItemHeight
        {
            get
            {
                return (double)this.GetValue(ItemHeightProperty);
            }
            set
            {
                this.SetValue(ItemHeightProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the width of the layout area for each item.
        /// </summary>
        public double ItemWidth
        {
            get
            {
                return (double)this.GetValue(ItemWidthProperty);
            }
            set
            {
                this.SetValue(ItemWidthProperty, value);
            }
        }
    }
}