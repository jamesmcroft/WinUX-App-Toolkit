namespace WinUX.Xaml.VisualStateTriggers.MaxWindowWidthTrigger
{
    using Windows.UI.Core;
    using Windows.UI.Xaml;

    /// <summary>
    /// Defines a visual state trigger that checks whether a condition is met within a specified max window width.
    /// </summary>
    /// <remarks>
    /// This is useful where the standard AdaptiveTrigger doesn't meet needs of combining a boolean condition with the window width.
    /// </remarks>
    public sealed partial class MaxWindowWidthTrigger : VisualStateTriggerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MaxWindowWidthTrigger"/> class.
        /// </summary>
        public MaxWindowWidthTrigger()
        {
            Window.Current.SizeChanged += this.Window_OnSizeChanged;
        }

        private void Window_OnSizeChanged(object sender, WindowSizeChangedEventArgs args)
        {
            if (args != null)
            {
                this.CheckTriggerState(this.Trigger, args.Size.Width);
            }
        }

        private void OnTriggerChanged(bool trigger)
        {
            this.CheckTriggerState(trigger, this.WindowWidth);
        }

        private void CheckTriggerState(bool trigger, double windowWidth)
        {
            var withinWindowBounds = this.MaxWindowWidth >= windowWidth;

            this.IsActive = trigger && withinWindowBounds;
        }
    }
}