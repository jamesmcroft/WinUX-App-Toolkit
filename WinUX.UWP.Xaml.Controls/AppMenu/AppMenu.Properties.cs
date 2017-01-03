namespace WinUX.Xaml.Controls
{
    using System.Collections.ObjectModel;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    using WinUX.Data;
    using WinUX.Mvvm.Input;
    using WinUX.Mvvm.Services;

    /// <summary>
    /// Defines the properties for the <see cref="AppMenu"/>.
    /// </summary>
    public sealed partial class AppMenu
    {
        private DelegateCommand appMenuCommand;

        private DelegateCommand<AppMenuButton> appButtonNavigationCommand;

        private INavigationService navigationService;

        /// <summary>
        /// Defines the dependency property for the <see cref="HeaderContent"/>.
        /// </summary>
        public static readonly DependencyProperty HeaderContentProperty =
            DependencyProperty.Register(
                nameof(HeaderContent),
                typeof(UIElement),
                typeof(AppMenu),
                new PropertyMetadata(null));

        /// <summary>
        /// Defines the dependency property for the <see cref="DisplayMode"/>.
        /// </summary>
        public static readonly DependencyProperty DisplayModeProperty = DependencyProperty.Register(
            nameof(DisplayMode),
            typeof(SplitViewDisplayMode),
            typeof(AppMenu),
            new PropertyMetadata(null, (d, e) => ((AppMenu)d).OnDisplayModeChanged((SplitViewDisplayMode)e.NewValue)));

        /// <summary>
        /// Defines the dependency property for the <see cref="VisualStateNarrowMinWidth"/>.
        /// </summary>
        public static readonly DependencyProperty VisualStateNarrowMinWidthProperty =
            DependencyProperty.Register(
                nameof(VisualStateNarrowMinWidth),
                typeof(double),
                typeof(AppMenu),
                new PropertyMetadata((double)-1));

        /// <summary>
        /// Defines the dependency property for the <see cref="VisualStateNormalMinWidth"/>.
        /// </summary>
        public static readonly DependencyProperty VisualStateNormalMinWidthProperty =
            DependencyProperty.Register(
                nameof(VisualStateNormalMinWidth),
                typeof(double),
                typeof(AppMenu),
                new PropertyMetadata((double)0));

        /// <summary>
        /// Defines the dependency property for the <see cref="OpenPaneWidth"/>.
        /// </summary>
        public static readonly DependencyProperty OpenPaneWidthProperty =
            DependencyProperty.Register(
                nameof(OpenPaneWidth),
                typeof(double),
                typeof(AppMenu),
                new PropertyMetadata(220d));

        /// <summary>
        /// Defines the dependency property for the <see cref="AppMenuButtonWidth"/>.
        /// </summary>
        public static readonly DependencyProperty AppMenuButtonWidthProperty =
            DependencyProperty.Register(
                nameof(AppMenuButtonWidth),
                typeof(double),
                typeof(AppMenu),
                new PropertyMetadata(48d));

        /// <summary>
        /// Defines the dependency property for the <see cref="PaneBorderThickness"/>.
        /// </summary>
        public static readonly DependencyProperty PaneBorderThicknessProperty =
            DependencyProperty.Register(
                nameof(PaneBorderThickness),
                typeof(Thickness),
                typeof(AppMenu),
                new PropertyMetadata(new Thickness(0, 0, 1, 0)));

        /// <summary>
        /// Defines the dependency property for the <see cref="SecondaryButtonOrientation"/>.
        /// </summary>
        public static readonly DependencyProperty SecondaryButtonOrientationProperty =
            DependencyProperty.Register(
                nameof(SecondaryButtonOrientation),
                typeof(Orientation),
                typeof(AppMenu),
                new PropertyMetadata(Orientation.Vertical));

        /// <summary>
        /// Defines the dependency property for the <see cref="PaneButtonVisibility"/>.
        /// </summary>
        public static readonly DependencyProperty PaneButtonVisibilityProperty =
            DependencyProperty.Register(
                nameof(PaneButtonVisibility),
                typeof(Visibility),
                typeof(AppMenu),
                new PropertyMetadata(
                    Visibility.Visible,
                    (d, e) => ((AppMenu)d).PaneButton.Visibility = (Visibility)e.NewValue));

        /// <summary>
        /// Defines the dependency property for the <see cref="IsPaneButtonEnabled"/>.
        /// </summary>
        public static readonly DependencyProperty IsPaneButtonEnabledProperty =
            DependencyProperty.Register(
                nameof(IsPaneButtonEnabled),
                typeof(bool),
                typeof(AppMenu),
                new PropertyMetadata(true));

        /// <summary>
        /// Defines the dependency property for the <see cref="SelectedButton"/>.
        /// </summary>
        public static readonly DependencyProperty SelectedButtonProperty =
            DependencyProperty.Register(
                nameof(SelectedButton),
                typeof(AppMenuButton),
                typeof(AppMenu),
                new PropertyMetadata(null, OnSelectedButtonChanged));

        /// <summary>
        /// Defines the dependency property for the <see cref="IsOpen"/>.
        /// </summary>
        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register(
            nameof(IsOpen),
            typeof(bool),
            typeof(AppMenu),
            new PropertyMetadata(
                false,
                (d, e) =>
                    {
                        var appMenu = d as AppMenu;
                        if (appMenu != null)
                        {
                            appMenu.IsOpen = (bool)e.NewValue;
                        }
                    }));

        /// <summary>
        /// Defines the dependency property for the <see cref="PrimaryButtons"/>.
        /// </summary>
        public static readonly DependencyProperty PrimaryButtonsProperty =
            DependencyProperty.Register(
                nameof(PrimaryButtons),
                typeof(ObservableCollection<AppMenuButton>),
                typeof(AppMenu),
                new PropertyMetadata(null));

        /// <summary>
        /// Defines the dependency property for the <see cref="SecondaryButtons"/>.
        /// </summary>
        public static readonly DependencyProperty SecondaryButtonsProperty =
            DependencyProperty.Register(
                nameof(SecondaryButtons),
                typeof(ObservableCollection<AppMenuButton>),
                typeof(AppMenu),
                new PropertyMetadata(null));

        /// <summary>
        /// Defines the dependency property for the <see cref="FlyoutButtons"/>.
        /// </summary>
        public static readonly DependencyProperty FlyoutButtonsProperty =
            DependencyProperty.Register(
                nameof(FlyoutButtons),
                typeof(ObservableCollection<FlyoutAppMenuButton>),
                typeof(AppMenu),
                new PropertyMetadata(null));

        public static readonly DependencyProperty ContentMarginProperty =
            DependencyProperty.Register(
                nameof(ContentMargin),
                typeof(Thickness),
                typeof(AppMenu),
                new PropertyMetadata(new Thickness(0), (d,e) => ((AppMenu)d).UpdateContentMargin((Thickness)e.NewValue)));

        /// <summary>
        /// Gets or sets the content margin.
        /// </summary>
        public Thickness ContentMargin
        {
            get
            {
                return (Thickness)this.GetValue(ContentMarginProperty);
            }
            set
            {
                this.SetValue(ContentMarginProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the content for the header.
        /// </summary>
        public UIElement HeaderContent
        {
            get
            {
                return (UIElement)this.GetValue(HeaderContentProperty);
            }
            set
            {
                this.SetValue(HeaderContentProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the display mode of the <see cref="AppMenu"/> pane.
        /// </summary>
        public SplitViewDisplayMode DisplayMode
        {
            get
            {
                return (SplitViewDisplayMode)this.GetValue(DisplayModeProperty);
            }
            set
            {
                this.SetValue(DisplayModeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the width to determine a narrow visual state.
        /// </summary>
        public double VisualStateNarrowMinWidth
        {
            get
            {
                return this.VisualStateNarrowTrigger.MinWindowWidth;
            }
            set
            {
                this.SetValue(VisualStateNarrowMinWidthProperty, this.VisualStateNarrowTrigger.MinWindowWidth = value);
            }
        }

        /// <summary>
        /// Gets or sets the width to determine a normal visual state.
        /// </summary>
        public double VisualStateNormalMinWidth
        {
            get
            {
                return this.VisualStateNormalTrigger.MinWindowWidth;
            }
            set
            {
                this.SetValue(VisualStateNormalMinWidthProperty, this.VisualStateNormalTrigger.MinWindowWidth = value);
            }
        }

        /// <summary>
        /// Gets or sets the width of the pane when open.
        /// </summary>
        public double OpenPaneWidth
        {
            get
            {
                return (double)this.GetValue(OpenPaneWidthProperty);
            }
            set
            {
                this.SetValue(OpenPaneWidthProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the width of the app menu buttons.
        /// </summary>
        public double AppMenuButtonWidth
        {
            get
            {
                return (double)this.GetValue(AppMenuButtonWidthProperty);
            }
            set
            {
                this.SetValue(AppMenuButtonWidthProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the thickness of the pane's border.
        /// </summary>
        public Thickness PaneBorderThickness
        {
            get
            {
                return (Thickness)this.GetValue(PaneBorderThicknessProperty);
            }
            set
            {
                this.SetValue(PaneBorderThicknessProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the orientation of the secondary buttons.
        /// </summary>
        public Orientation SecondaryButtonOrientation
        {
            get
            {
                return (Orientation)this.GetValue(SecondaryButtonOrientationProperty);
            }
            set
            {
                this.SetValue(SecondaryButtonOrientationProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the visibility of the pane button.
        /// </summary>
        public Visibility PaneButtonVisibility
        {
            get
            {
                return (Visibility)this.GetValue(PaneButtonVisibilityProperty);
            }
            set
            {
                this.SetValue(PaneButtonVisibilityProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the enabled state of the pane button.
        /// </summary>
        public bool IsPaneButtonEnabled
        {
            get
            {
                return (bool)this.GetValue(IsPaneButtonEnabledProperty);
            }
            set
            {
                this.SetValue(IsPaneButtonEnabledProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the current selected button.
        /// </summary>
        public AppMenuButton SelectedButton
        {
            get
            {
                return this.GetValue(SelectedButtonProperty) as AppMenuButton;
            }
            set
            {
                var oldValue = this.SelectedButton;

                this.SetValue(SelectedButtonProperty, value);
                this.MenuItemSelectionChanged?.Invoke(this, new ValueChangedEventArgs<AppMenuButton>(oldValue, value));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the pane is open.
        /// </summary>
        public bool IsOpen
        {
            get
            {
                var open = this.MenuShell.IsPaneOpen;
                if (open != (bool)this.GetValue(IsOpenProperty))
                {
                    this.SetValue(IsOpenProperty, open);
                }

                return open;
            }
            set
            {
                var open = this.MenuShell.IsPaneOpen;
                if (open == value)
                {
                    return;
                }

                this.SetValue(IsOpenProperty, value);

                if (value)
                {
                    this.MenuShell.IsPaneOpen = true;
                    this.AppMenuButtonWidth = this.MenuShell.DisplayMode == SplitViewDisplayMode.CompactInline
                                                  ? this.OpenPaneWidth
                                                  : 48;
                }
                else
                {
                    if (this.MenuShell.DisplayMode == SplitViewDisplayMode.Overlay && this.MenuShell.IsPaneOpen
                        || (this.MenuShell.DisplayMode == SplitViewDisplayMode.CompactOverlay
                            && this.MenuShell.IsPaneOpen)
                        || (this.MenuShell.DisplayMode == SplitViewDisplayMode.CompactInline
                            && this.MenuShell.IsPaneOpen))
                    {
                        this.MenuShell.IsPaneOpen = false;
                    }

                    this.AppMenuButtonWidth = 48;
                }
            }
        }

        /// <summary>
        /// Gets or sets the primary buttons collection.
        /// </summary>
        public ObservableCollection<AppMenuButton> PrimaryButtons
        {
            get
            {
                var primaryButtons = (ObservableCollection<AppMenuButton>)this.GetValue(PrimaryButtonsProperty);
                if (primaryButtons == null)
                {
                    this.SetValue(PrimaryButtonsProperty, primaryButtons = new ObservableCollection<AppMenuButton>());
                }
                return primaryButtons;
            }
            set
            {
                this.SetValue(PrimaryButtonsProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the secondary buttons collection.
        /// </summary>
        public ObservableCollection<AppMenuButton> SecondaryButtons
        {
            get
            {
                var secondaryButtons = (ObservableCollection<AppMenuButton>)this.GetValue(SecondaryButtonsProperty);
                if (secondaryButtons == null)
                {
                    this.SetValue(
                        SecondaryButtonsProperty,
                        secondaryButtons = new ObservableCollection<AppMenuButton>());
                }
                return secondaryButtons;
            }
            set
            {
                this.SetValue(SecondaryButtonsProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the flyout buttons collection.
        /// </summary>
        public ObservableCollection<FlyoutAppMenuButton> FlyoutButtons
        {
            get
            {
                var flyoutButtons = (ObservableCollection<FlyoutAppMenuButton>)this.GetValue(FlyoutButtonsProperty);
                if (flyoutButtons == null)
                {
                    this.SetValue(
                        FlyoutButtonsProperty,
                        flyoutButtons = new ObservableCollection<FlyoutAppMenuButton>());
                }
                return flyoutButtons;
            }
            set
            {
                this.SetValue(FlyoutButtonsProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the menu's navigation service.
        /// </summary>
        public INavigationService NavigationService
        {
            get
            {
                return this.navigationService;
            }
            set
            {
                if (this.navigationService != null)
                {
                    this.navigationService.Frame.Navigated -= this.OnFrameOnNavigated;
                }

                this.navigationService = value;

                var newFrame = value.Frame;
                newFrame.Margin = this.ContentMargin;

                this.MenuShell.Content = newFrame;

                this.NavigationService.Frame.Navigated += this.OnFrameOnNavigated;
            }
        }

        private void OnFrameOnNavigated(object s, NavigationEventArgs e)
        {
            this.UpdateSelectedButton(e.SourcePageType);
        }

        /// <summary>
        /// Gets the command called to perform a navigation based on an <see cref="AppMenuButton"/>.
        /// </summary>
        public DelegateCommand<AppMenuButton> AppButtonNavigationCommand
            =>
            this.appButtonNavigationCommand
            ?? (this.appButtonNavigationCommand = new DelegateCommand<AppMenuButton>(this.ExecutePageNavigation));

        internal DelegateCommand AppMenuCommand
            => this.appMenuCommand ?? (this.appMenuCommand = new DelegateCommand(this.ExecuteAppMenuAction));

        private int AppMenuButtonCount => this.PrimaryButtons.Count + this.SecondaryButtons.Count;
    }
}