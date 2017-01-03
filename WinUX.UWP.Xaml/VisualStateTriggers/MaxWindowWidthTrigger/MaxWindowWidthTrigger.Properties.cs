namespace WinUX.Xaml.VisualStateTriggers.MaxWindowWidthTrigger
{
    using Windows.UI.Xaml;

    /// <summary>
    /// Defines the properties for the <see cref="MaxWindowWidthTrigger"/>.
    /// </summary>
    public sealed partial class MaxWindowWidthTrigger
    {
        /// <summary>
        /// Defines the dependency property for <see cref="Trigger"/>.
        /// </summary>
        public static readonly DependencyProperty TriggerProperty = DependencyProperty.Register(
            nameof(Trigger),
            typeof(bool),
            typeof(MaxWindowWidthTrigger),
            new PropertyMetadata(
                false,
                (d, e) =>
                    {
                        ((MaxWindowWidthTrigger)d).OnTriggerChanged((bool)e.NewValue);
                    }));

        /// <summary>
        /// Defines the dependency property for <see cref="MaxWindowWidth"/>.
        /// </summary>
        public static readonly DependencyProperty MaxWindowWidthProperty =
            DependencyProperty.Register(
                nameof(MaxWindowWidth),
                typeof(double),
                typeof(MaxWindowWidthTrigger),
                new PropertyMetadata(0));

        /// <summary>
        /// Gets or sets the trigger.
        /// </summary>
        public bool Trigger
        {
            get
            {
                return (bool)this.GetValue(TriggerProperty);
            }
            set
            {
                this.SetValue(TriggerProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum window width to check.
        /// </summary>
        public double MaxWindowWidth
        {
            get
            {
                return (double)this.GetValue(MaxWindowWidthProperty);
            }
            set
            {
                this.SetValue(MaxWindowWidthProperty, value);
            }
        }

        /// <summary>
        /// Gets the current window width.
        /// </summary>
        public double WindowWidth => Window.Current.Bounds.Width;
    }
}