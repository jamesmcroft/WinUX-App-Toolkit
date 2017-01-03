namespace WinUX.Xaml.Behaviors.CommandBar
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using Microsoft.Xaml.Interactivity;

    /// <summary>
    /// Defines a behavior for command bars for triggering the open/hidden state.
    /// </summary>
    public sealed class CommandBarClosedModeTriggerBehavior : Behavior
    {
        /// <summary>
        /// Defines the dependency property for <see cref="IsOpen"/>.
        /// </summary>
        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register(
            nameof(IsOpen),
            typeof(bool),
            typeof(CommandBarClosedModeTriggerBehavior),
            new PropertyMetadata(
                false,
                (d, e) => ((CommandBarClosedModeTriggerBehavior)d).OnIsOpenChanged((bool)e.NewValue)));

        private CommandBar CommandBar => this.AssociatedObject as CommandBar;

        /// <summary>
        /// Gets or sets a value indicating whether the associated command bar is open.
        /// </summary>
        public bool IsOpen
        {
            get
            {
                return (bool)this.GetValue(IsOpenProperty);
            }
            set
            {
                this.SetValue(IsOpenProperty, value);
            }
        }

        private void OnIsOpenChanged(bool isOpen)
        {
            if (this.CommandBar != null)
            {
                this.CommandBar.ClosedDisplayMode = isOpen
                                                        ? AppBarClosedDisplayMode.Compact
                                                        : AppBarClosedDisplayMode.Hidden;
            }
        }
    }
}