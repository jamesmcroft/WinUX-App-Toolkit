namespace WinUX.Xaml.Controls
{
    using System.Collections.ObjectModel;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Defines a flyout button for the AppMenu control.
    /// </summary>
    public sealed class FlyoutAppMenuButton : AppMenuButton
    {
        /// <summary>
        /// Defines the dependency property for the <see cref="FlyoutItems"/>.
        /// </summary>
        public static readonly DependencyProperty FlyoutItemsProperty = DependencyProperty.Register(
            nameof(FlyoutItems),
            typeof(ObservableCollection<MenuFlyoutItem>),
            typeof(FlyoutAppMenuButton),
            new PropertyMetadata(null));

        /// <summary>
        /// Initializes a new instance of the <see cref="FlyoutAppMenuButton"/> class.
        /// </summary>
        public FlyoutAppMenuButton()
        {
            this.FlyoutItems = new ObservableCollection<MenuFlyoutItem>();
        }

        /// <summary>
        /// Gets or sets the flyout items.
        /// </summary>
        public ObservableCollection<MenuFlyoutItem> FlyoutItems
        {
            get
            {
                var primaryButtons = (ObservableCollection<MenuFlyoutItem>)this.GetValue(FlyoutItemsProperty);
                if (primaryButtons == null)
                {
                    this.SetValue(FlyoutItemsProperty, primaryButtons = new ObservableCollection<MenuFlyoutItem>());
                }
                return primaryButtons;
            }
            set
            {
                this.SetValue(FlyoutItemsProperty, value);
            }
        }
    }
}