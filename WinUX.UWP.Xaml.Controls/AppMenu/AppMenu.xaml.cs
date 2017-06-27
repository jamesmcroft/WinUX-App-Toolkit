namespace WinUX.Xaml.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Windows.UI;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Markup;
    using Windows.UI.Xaml.Media;

    using WinUX.Collections.ObjectModel;
    using WinUX.Data;
    using WinUX.Data.Serialization;
    using WinUX.Diagnostics.Tracing;

    /// <summary>
    /// Defines a control for providing an application menu system.
    /// </summary>
    [ContentProperty(Name = nameof(PrimaryButtons))]
    public sealed partial class AppMenu
    {
        private readonly Dictionary<RadioButton, AppMenuButton> navButtons =
            new Dictionary<RadioButton, AppMenuButton>();

        private bool areAppMenuButtonsLoaded;

        private int appMenuButtonsLoadedCounter;

        private bool isInsideOperation;

        private StackPanel secondaryButtonStackPanel;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppMenu"/> class.
        /// </summary>
        public AppMenu()
        {
            this.InitializeComponent();

            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                return;
            }

            this.PrimaryButtons = new ObservableItemCollection<AppMenuButton>();
            this.SecondaryButtons = new ObservableItemCollection<AppMenuButton>();

            this.MenuShell.RegisterPropertyChangedCallback(SplitView.IsPaneOpenProperty, this.OnShellPaneOpenChanged);
            this.MenuShell.RegisterPropertyChangedCallback(
                SplitView.DisplayModeProperty,
                (d, e) =>
                    {
                        this.DisplayMode = this.MenuShell.DisplayMode;
                    });

            this.Loaded += this.OnAppMenuLoaded;
        }

        /// <summary>
        /// The <see cref="AppMenu"/> pane opened event.
        /// </summary>
        public event EventHandler PaneOpened;

        /// <summary>
        /// The <see cref="AppMenu"/> pane closed event.
        /// </summary>
        public event EventHandler PaneClosed;

        /// <summary>
        /// The <see cref="AppMenu"/> selected menu item changed event.
        /// </summary>
        public event ValueChangedEventHandler<AppMenuButton> MenuItemSelectionChanged;

        private void OnShellPaneOpenChanged(DependencyObject d, DependencyProperty e)
        {
            if (this.secondaryButtonStackPanel == null)
            {
                return;
            }

            if (this.SecondaryButtonOrientation.Equals(Orientation.Horizontal) && this.MenuShell.IsPaneOpen)
            {
                this.secondaryButtonStackPanel.Orientation = Orientation.Horizontal;
            }
            else
            {
                this.secondaryButtonStackPanel.Orientation = Orientation.Vertical;
            }

            var splitView = d as SplitView;
            if (splitView != null && splitView.IsPaneOpen)
            {
                this.PaneOpened?.Invoke(this.MenuShell, EventArgs.Empty);
                this.AppMenuButtonWidth = (this.MenuShell.DisplayMode == SplitViewDisplayMode.CompactInline)
                                              ? this.OpenPaneWidth
                                              : 48;
            }
            else
            {
                this.PaneClosed?.Invoke(this.MenuShell, EventArgs.Empty);
            }

            if (!d.GetValue(e).Equals(this.IsOpen))
            {
                this.IsOpen = !this.IsOpen;
            }
        }

        private void OnAppMenuLoaded(object s, RoutedEventArgs e)
        {
            var any =
                this.GetType()
                    .GetRuntimeProperties()
                    .Where(x => x.PropertyType == typeof(SolidColorBrush))
                    .Any(x => x.GetValue(this) != null);

            if (!any)
            {
                this.AccentColor = (Color)this.Resources["SystemAccentColor"];
            }

            if (this.AppMenuButtonCount == 0)
            {
                this.areAppMenuButtonsLoaded = true;
            }
        }

        private void OnDisplayModeChanged(SplitViewDisplayMode displayMode)
        {
            if (this.MenuShell.DisplayMode != displayMode)
            {
                this.MenuShell.DisplayMode = displayMode;
            }

            this.AppMenuButtonWidth = this.MenuShell.DisplayMode == SplitViewDisplayMode.CompactInline
                                          ? this.OpenPaneWidth
                                          : 48;
        }

        internal void UpdateSelectedButton(Type pageType = null, object pageParam = null)
        {
            if (this.NavigationService != null)
            {
                pageType = pageType ?? this.NavigationService.CurrentPageType;
                var typeMatchButtons = this.navButtons.Where(x => x.Value.Page == pageType).ToList();

                if (pageParam == null)
                {
                    pageParam = this.NavigationService.CurrentPageNavigationParameter;
                }
                else
                {
                    try
                    {
                        pageParam = SerializationService.Json.Deserialize(pageParam.ToString());
                    }
                    catch (Exception ex)
                    {
#if DEBUG
                        System.Diagnostics.Debug.WriteLine(ex.ToString());
#endif
                    }
                }

                var paramMatchButtons =
                    typeMatchButtons.Where(
                        x => Equals(x.Value.PageParameter, null) || Equals(x.Value.PageParameter, pageParam));

                var button = paramMatchButtons.Select(x => x.Value).FirstOrDefault()
                             ?? typeMatchButtons.Select(x => x.Value).FirstOrDefault();

                this.SelectedButton = button;
            }
        }

        private void ExecuteAppMenuAction()
        {
            this.IsOpen = !this.IsOpen;
        }

        private void ExecutePageNavigation(AppMenuButton menuButton)
        {
            if (menuButton == null)
            {
                throw new NullReferenceException("CommandParameter is not set");
            }

            if (menuButton.ButtonType == AppMenuButtonType.Command)
            {
                menuButton.Command?.Execute(menuButton.CommandParameter);
                return;
            }

            if (menuButton.Page != null)
            {
                this.SelectedButton = menuButton;
            }
        }

        private static void OnSelectedButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var appMenu = d as AppMenu;
            if (appMenu == null)
            {
                return;
            }

            appMenu.isInsideOperation = true;

            try
            {
                appMenu.SetSelectedButton((AppMenuButton)e.OldValue, (AppMenuButton)e.NewValue);
            }
            catch (Exception ex)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.ToString());
#endif
            }
            finally
            {
                appMenu.isInsideOperation = false;
            }
        }

        private void SetSelectedButton(AppMenuButton previous, AppMenuButton value)
        {
            if (previous != null)
            {
                this.IsOpen = this.DisplayMode == SplitViewDisplayMode.CompactInline && this.IsOpen;
            }

            if (previous?.IsChecked ?? previous != value)
            {
                previous?.RaiseUnselected();
            }

            this.navButtons.Where(x => x.Value != value).ForEach(x => { x.Value.IsChecked = false; });

            if (this.areAppMenuButtonsLoaded && value?.Page != null)
            {
                if (this.NavigationService.CurrentPageType == value.Page)
                {
                    // Want to make sure the item is selected
                    value.IsChecked = value.ButtonType == AppMenuButtonType.Toggle;
                    return;
                }

                var navigated = this.NavigationService.Navigate(value.Page, value.PageParameter);
                if (!navigated)
                {
                    this.SelectedButton = previous;
                    return;
                }

                this.IsOpen = this.DisplayMode == SplitViewDisplayMode.CompactInline && this.IsOpen;
                if (value.ClearNavigationStack)
                {
                    this.NavigationService.ClearNavigationHistory();
                }
            }

            if (value != null)
            {
                value.IsChecked = (value.ButtonType == AppMenuButtonType.Toggle);
                if (previous != value)
                {
                    value.RaiseSelected();
                }
            }
        }

        private void OnAppMenuButtonLoaded(object sender, RoutedEventArgs e)
        {
            var button = sender as RadioButton;
            if (button != null)
            {
                var appMenuButton = button.DataContext as AppMenuButton;
                if (!this.navButtons.ContainsKey(button))
                {
                    this.navButtons.Add(button, appMenuButton);
                    if (!this.areAppMenuButtonsLoaded)
                    {
                        this.appMenuButtonsLoadedCounter++;
                        if (this.appMenuButtonsLoadedCounter >= this.AppMenuButtonCount)
                        {
                            this.areAppMenuButtonsLoaded = true;
                        }
                    }
                }
            }
            this.UpdateSelectedButton();
        }

        private void OnAppMenuButtonTapped(object sender, TappedRoutedEventArgs e)
        {
            var button = sender as RadioButton;
            if (button != null)
            {
                var appMenuButton = button.DataContext as AppMenuButton;
                appMenuButton?.RaiseTapped(e);
            }

            e.Handled = true;
        }

        private void OnAppMenuButtonRightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            var button = sender as RadioButton;
            if (button != null)
            {
                var appMenuButton = button.DataContext as AppMenuButton;
                appMenuButton?.RaiseRightTapped(e);
            }

            e.Handled = true;
        }

        private void OnAppMenuButtonHolding(object sender, HoldingRoutedEventArgs e)
        {
            var button = sender as RadioButton;
            if (button != null)
            {
                var appMenuButton = button.DataContext as AppMenuButton;
                appMenuButton?.RaiseHolding(e);
            }

            e.Handled = true;
        }

        private void OnSecondaryButtonStackPanelLoaded(object sender, RoutedEventArgs e)
        {
            this.secondaryButtonStackPanel = sender as StackPanel;
        }

        private void OnAppMenuButtonChecked(object sender, RoutedEventArgs e)
        {
            if (this.isInsideOperation)
            {
                return;
            }

            this.isInsideOperation = true;

            try
            {
                var button = sender as Windows.UI.Xaml.Controls.Primitives.ToggleButton;
                if (button != null)
                {
                    var appMenuButton = button.DataContext as AppMenuButton;
                    button.IsChecked = appMenuButton != null && (appMenuButton.ButtonType == AppMenuButtonType.Toggle);

                    if ((button.IsChecked ?? true) && appMenuButton != null)
                    {
                        this.UpdateSelectedButton();
                    }

                    button.IsChecked = Equals(appMenuButton, this.SelectedButton);

                    if (button.IsChecked ?? true)
                    {
                        appMenuButton?.RaiseChecked(e);
                    }
                }
            }
            finally
            {
                this.isInsideOperation = false;
            }
        }

        private void OnAppMenuButtonUnchecked(object sender, RoutedEventArgs e)
        {
            if (this.isInsideOperation)
            {
                return;
            }

            this.isInsideOperation = true;

            try
            {
                var button = sender as Windows.UI.Xaml.Controls.Primitives.ToggleButton;
                if (button != null)
                {
                    var appMenuButton = button.DataContext as AppMenuButton;

                    if (button.FocusState != FocusState.Unfocused)
                    {
                        button.IsChecked = appMenuButton != null
                                           && (appMenuButton.ButtonType == AppMenuButtonType.Toggle);
                        this.IsOpen = false;
                        return;
                    }

                    appMenuButton?.RaiseUnchecked(e);
                }

                this.UpdateSelectedButton();
            }
            finally
            {
                this.isInsideOperation = false;
            }
        }

        private void UpdateContentMargin(Thickness contentMargin)
        {
            var content = this.MenuShell.Content as Frame;
            if (content != null)
            {
                content.Margin = contentMargin;
            }
        }
    }
}