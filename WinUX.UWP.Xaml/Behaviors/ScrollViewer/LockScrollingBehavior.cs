namespace WinUX.Xaml.Behaviors.ScrollViewer
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using Microsoft.Xaml.Interactivity;

    using WinUX.Controls.ScrollViewer;

    /// <summary>
    /// Defines a behavior for locking the scrolling of a ScrollViewer control when triggered.
    /// </summary>
    public sealed class LockScrollingBehavior : Behavior
    {
        /// <summary>
        /// Defines the dependency property for <see cref="Trigger"/>.
        /// </summary>
        public static readonly DependencyProperty TriggerProperty = DependencyProperty.Register(
            nameof(Trigger),
            typeof(bool),
            typeof(LockScrollingBehavior),
            new PropertyMetadata(false, (d, e) => ((LockScrollingBehavior)d).UpdateScrollMode((bool)e.NewValue)));

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
        /// Gets or sets the default scroll mode to return to when unlocked.
        /// </summary>
        public ScrollViewerMode ScrollMode { get; set; }

        private ScrollViewer ScrollViewer => this.AssociatedObject as ScrollViewer;

        private void UpdateScrollMode(bool shouldLock)
        {
            if (this.ScrollViewer == null) return;

            if (shouldLock)
            {
                this.ScrollViewer.Lock();
            }
            else
            {
                this.ScrollViewer.UpdateScrollMode(this.ScrollMode);
            }
        }
    }
}