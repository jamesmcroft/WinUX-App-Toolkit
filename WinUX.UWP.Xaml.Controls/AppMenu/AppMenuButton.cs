namespace WinUX.Xaml.Controls
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Markup;

    using WinUX.Mvvm;

    /// <summary>
    /// Defines a button for the AppMenu control.
    /// </summary>
    [ContentProperty(Name = nameof(Content))]
    public partial class AppMenuButton : BindableBase
    {
        /// <summary>
        /// The button selected event.
        /// </summary>
        public event RoutedEventHandler Selected;

        /// <summary>
        /// The button unselected event.
        /// </summary>
        public event RoutedEventHandler Unselected;

        /// <summary>
        /// The button checked event.
        /// </summary>
        public event RoutedEventHandler Checked;

        /// <summary>
        /// The button unchecked event.
        /// </summary>
        public event RoutedEventHandler Unchecked;

        /// <summary>
        /// The button tapped event.
        /// </summary>
        public event RoutedEventHandler Tapped;

        /// <summary>
        /// The button right tapped event.
        /// </summary>
        public event RightTappedEventHandler RightTapped;

        /// <summary>
        /// The button holding event.
        /// </summary>
        public event HoldingEventHandler Holding;

        internal void RaiseSelected()
        {
            this.Selected?.Invoke(this, new RoutedEventArgs());
        }

        internal void RaiseUnselected()
        {
            this.Unselected?.Invoke(this, new RoutedEventArgs());
        }

        internal void RaiseChecked(RoutedEventArgs args)
        {
            if (this.ButtonType == AppMenuButtonType.Toggle)
            {
                this.Checked?.Invoke(this, args);
            }
        }

        internal void RaiseUnchecked(RoutedEventArgs args)
        {
            if (this.ButtonType == AppMenuButtonType.Toggle)
            {
                this.Unchecked?.Invoke(this, args);
            }
        }

        internal void RaiseTapped(RoutedEventArgs args)
        {
            this.Tapped?.Invoke(this, args);
        }

        internal void RaiseRightTapped(RightTappedRoutedEventArgs args)
        {
            this.RightTapped?.Invoke(this, args);
        }

        internal void RaiseHolding(HoldingRoutedEventArgs args)
        {
            this.Holding?.Invoke(this, args);
        }
    }
}