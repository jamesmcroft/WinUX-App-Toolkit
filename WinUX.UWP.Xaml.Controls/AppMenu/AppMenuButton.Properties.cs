namespace WinUX.Xaml.Controls
{
    using System;
    using System.Windows.Input;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Media.Animation;

    /// <summary>
    /// Defines the properties for the <see cref="AppMenuButton"/>.
    /// </summary>
    public partial class AppMenuButton
    {
        private UIElement content;

        private Type page;

        private bool clearNavigationStack;

        private bool? isChecked;

        private double maxWidth = 9999;

        /// <summary>
        /// Defines the dependency property for the <see cref="PageParameter"/>.
        /// </summary>
        public static readonly DependencyProperty PageParameterProperty =
            DependencyProperty.Register(
                nameof(PageParameter),
                typeof(object),
                typeof(AppMenuButton),
                new PropertyMetadata(null));

        /// <summary>
        /// Defines the dependency property for the <see cref="Visibility"/>.
        /// </summary>
        public static readonly DependencyProperty VisibilityProperty = DependencyProperty.Register(
            nameof(Visibility),
            typeof(Visibility),
            typeof(AppMenuButton),
            new PropertyMetadata(Visibility.Visible));

        /// <summary>
        /// Defines the dependency property for the <see cref="IsEnabled"/>.
        /// </summary>
        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.Register(
            nameof(IsEnabled),
            typeof(bool),
            typeof(AppMenuButton),
            new PropertyMetadata(true));

        private string toolTip;

        private bool isGrouped;

        /// <summary>
        /// Gets or sets the tooltip text.
        /// </summary>
        public string ToolTip
        {
            get
            {
                return this.toolTip;
            }
            set
            {
                this.Set(() => this.ToolTip, ref this.toolTip, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the button is grouped.
        /// </summary>
        public bool IsGrouped
        {
            get
            {
                return this.isGrouped;
            }
            set
            {
                this.Set(() => this.IsGrouped, ref this.isGrouped, value);
            }
        }

        /// <summary>
        /// Gets or sets the content of the button.
        /// </summary>
        public UIElement Content
        {
            get
            {
                return this.content;
            }
            set
            {
                this.Set(() => this.Content, ref this.content, value);
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of button.
        /// </summary>
        public AppMenuButtonType ButtonType { get; set; } = AppMenuButtonType.Toggle;

        /// <summary>
        /// Gets or sets the command called when the button is clicked.
        /// </summary>
        public ICommand Command { get; set; }

        /// <summary>
        /// Gets or sets the parameter to pass to the command.
        /// </summary>
        public object CommandParameter { get; set; }

        /// <summary>
        /// Gets or sets the page type associated with the button.
        /// </summary>
        public Type Page
        {
            get
            {
                return this.page;
            }
            set
            {
                this.Set(() => this.Page, ref this.page, value);
            }
        }

        /// <summary>
        /// Gets or sets the parameter to pass as part of the page navigation.
        /// </summary>
        public object PageParameter
        {
            get
            {
                return this.GetValue(PageParameterProperty);
            }
            set
            {
                this.SetValue(PageParameterProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the navigation transition used as part of the page navigation.
        /// </summary>
        public NavigationTransitionInfo NavigationTransitionInfo { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to clear the navigation stack when the associated page is navigated to.
        /// </summary>
        public bool ClearNavigationStack
        {
            get
            {
                return this.clearNavigationStack;
            }
            set
            {
                this.Set(() => this.ClearNavigationStack, ref this.clearNavigationStack, value);
            }
        }

        /// <summary>
        /// Gets or sets the visibility of the button.
        /// </summary>
        public Visibility Visibility
        {
            get
            {
                return (Visibility)this.GetValue(VisibilityProperty);
            }
            set
            {
                this.SetValue(VisibilityProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets whether the button is enabled.
        /// </summary>
        public bool IsEnabled
        {
            get
            {
                return (bool)this.GetValue(IsEnabledProperty);
            }
            set
            {
                this.SetValue(IsEnabledProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets whether the button is checked.
        /// </summary>
        public bool? IsChecked
        {
            get
            {
                return this.isChecked;
            }
            set
            {
                this.Set(() => this.IsChecked, ref this.isChecked, value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum width of the button.
        /// </summary>
        public double MaxWidth
        {
            get
            {
                return this.maxWidth;
            }
            set
            {
                this.Set(() => this.MaxWidth, ref this.maxWidth, value);
            }
        }
    }
}