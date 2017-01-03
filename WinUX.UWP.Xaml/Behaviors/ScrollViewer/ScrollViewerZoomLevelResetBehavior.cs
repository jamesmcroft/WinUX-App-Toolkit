namespace WinUX.Xaml.Behaviors.ScrollViewer
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using Microsoft.Xaml.Interactivity;

    /// <summary>
    /// Defines a behavior for reseting the zoom level of a ScrollViewer control when triggered.
    /// </summary>
    public sealed class ScrollViewerZoomLevelResetBehavior : Behavior
    {
        /// <summary>
        /// Defines the dependency property for <see cref="Trigger"/>.
        /// </summary>
        public static readonly DependencyProperty TriggerProperty = DependencyProperty.Register(
            nameof(Trigger),
            typeof(bool),
            typeof(ScrollViewerZoomLevelResetBehavior),
            new PropertyMetadata(false, (d, e) => { ((ScrollViewerZoomLevelResetBehavior)d).ResetZoomLevel(); }));

        /// <summary>
        /// Defines the dependency property for <see cref="DefaultZoomLevel"/>.
        /// </summary>
        public static readonly DependencyProperty DefaultZoomLevelProperty =
            DependencyProperty.Register(
                nameof(DefaultZoomLevel),
                typeof(float),
                typeof(ScrollViewerZoomLevelResetBehavior),
                new PropertyMetadata(1.0));

        /// <summary>
        /// Gets or sets the default zoom level to reset to.
        /// </summary>
        public float DefaultZoomLevel
        {
            get
            {
                return (float)this.GetValue(DefaultZoomLevelProperty);
            }
            set
            {
                this.SetValue(DefaultZoomLevelProperty, value);
            }
        }

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

        private ScrollViewer ScrollViewer => this.AssociatedObject as ScrollViewer;

        /// <summary>
        /// Called after the behavior is attached to the <see cref="P:Microsoft.Xaml.Interactivity.Behavior.AssociatedObject" />.
        /// </summary>
        protected override void OnAttached()
        {
            this.ResetZoomLevel();
        }

        private void ResetZoomLevel()
        {
            this.ScrollViewer?.ZoomToFactor(this.DefaultZoomLevel);
        }
    }
}