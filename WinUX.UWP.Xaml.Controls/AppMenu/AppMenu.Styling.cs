namespace WinUX.Xaml.Controls
{
    using Windows.UI;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Media;

    /// <summary>
    /// Defines the styling properties for the <see cref="AppMenu"/>.
    /// </summary>
    public sealed partial class AppMenu
    {
        /// <summary>
        /// Defines the dependency property for the <see cref="AccentColor"/>.
        /// </summary>
        public static readonly DependencyProperty AccentColorProperty = DependencyProperty.Register(
            nameof(AccentColor),
            typeof(Color),
            typeof(AppMenu),
            new PropertyMetadata(
                null,
                (d, e) =>
                    {
                        var appMenu = d as AppMenu;
                        appMenu?.RefreshStyles((Color)e.NewValue);
                    }));

        /// <summary>
        /// Defines the dependency property for the <see cref="PaneButtonBackground"/>.
        /// </summary>
        public static readonly DependencyProperty PaneButtonBackgroundProperty =
            DependencyProperty.Register(
                nameof(PaneButtonBackground),
                typeof(SolidColorBrush),
                typeof(AppMenu),
                new PropertyMetadata(null));

        /// <summary>
        /// Defines the dependency property for the <see cref="PaneButtonForeground"/>.
        /// </summary>
        public static readonly DependencyProperty PaneButtonForegroundProperty =
            DependencyProperty.Register(
                nameof(PaneButtonForeground),
                typeof(SolidColorBrush),
                typeof(AppMenu),
                new PropertyMetadata(null));

        /// <summary>
        /// Defines the dependency property for the <see cref="PaneBackground"/>.
        /// </summary>
        public static readonly DependencyProperty PaneBackgroundProperty =
            DependencyProperty.Register(
                nameof(PaneBackground),
                typeof(SolidColorBrush),
                typeof(AppMenu),
                new PropertyMetadata(null));

        /// <summary>
        /// Defines the dependency property for the <see cref="PaneBorderBrush"/>.
        /// </summary>
        public static readonly DependencyProperty PaneBorderBrushProperty =
            DependencyProperty.Register(
                nameof(PaneBorderBrush),
                typeof(SolidColorBrush),
                typeof(AppMenu),
                new PropertyMetadata(null));

        /// <summary>
        /// Defines the dependency property for the <see cref="AppMenuButtonBackground"/>.
        /// </summary>
        public static readonly DependencyProperty AppMenuButtonBackgroundProperty =
            DependencyProperty.Register(
                nameof(AppMenuButtonBackground),
                typeof(SolidColorBrush),
                typeof(AppMenu),
                new PropertyMetadata(null));

        /// <summary>
        /// Defines the dependency property for the <see cref="AppMenuButtonCheckedBackground"/>.
        /// </summary>
        public static readonly DependencyProperty AppMenuButtonCheckedBackgroundProperty =
            DependencyProperty.Register(
                nameof(AppMenuButtonCheckedBackground),
                typeof(SolidColorBrush),
                typeof(AppMenu),
                new PropertyMetadata(null));

        /// <summary>
        /// Defines the dependency property for the <see cref="AppMenuButtonPressedBackground"/>.
        /// </summary>
        public static readonly DependencyProperty AppMenuButtonPressedBackgroundProperty =
            DependencyProperty.Register(
                nameof(AppMenuButtonPressedBackground),
                typeof(SolidColorBrush),
                typeof(AppMenu),
                new PropertyMetadata(null));

        /// <summary>
        /// Defines the dependency property for the <see cref="AppMenuButtonHoverBackground"/>.
        /// </summary>
        public static readonly DependencyProperty AppMenuButtonHoverBackgroundProperty =
            DependencyProperty.Register(
                nameof(AppMenuButtonHoverBackground),
                typeof(SolidColorBrush),
                typeof(AppMenu),
                new PropertyMetadata(null));

        /// <summary>
        /// Defines the dependency property for the <see cref="AppMenuButtonForeground"/>.
        /// </summary>
        public static readonly DependencyProperty AppMenuButtonForegroundProperty =
            DependencyProperty.Register(
                nameof(AppMenuButtonForeground),
                typeof(SolidColorBrush),
                typeof(AppMenu),
                new PropertyMetadata(null));

        /// <summary>
        /// Defines the dependency property for the <see cref="AppMenuButtonCheckedForeground"/>.
        /// </summary>
        public static readonly DependencyProperty AppMenuButtonCheckedForegroundProperty =
            DependencyProperty.Register(
                nameof(AppMenuButtonCheckedForeground),
                typeof(SolidColorBrush),
                typeof(AppMenu),
                new PropertyMetadata(null));

        /// <summary>
        /// Defines the dependency property for the <see cref="SecondarySeparatorColor"/>.
        /// </summary>
        public static readonly DependencyProperty SecondarySeparatorColorProperty =
            DependencyProperty.Register(
                nameof(SecondarySeparatorColor),
                typeof(SolidColorBrush),
                typeof(AppMenu),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the accent color for the pane.
        /// </summary>
        public Color AccentColor
        {
            get
            {
                return (Color)this.GetValue(AccentColorProperty);
            }
            set
            {
                this.SetValue(AccentColorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the background color of the pane button.
        /// </summary>
        public SolidColorBrush PaneButtonBackground
        {
            get
            {
                return this.GetValue(PaneButtonBackgroundProperty) as SolidColorBrush;
            }
            set
            {
                this.SetValue(PaneButtonBackgroundProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the foreground color of the pane button.
        /// </summary>
        public SolidColorBrush PaneButtonForeground
        {
            get
            {
                return this.GetValue(PaneButtonForegroundProperty) as SolidColorBrush;
            }
            set
            {
                this.SetValue(PaneButtonForegroundProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the background color of the pane.
        /// </summary>
        public SolidColorBrush PaneBackground
        {
            get
            {
                return this.GetValue(PaneBackgroundProperty) as SolidColorBrush;
            }
            set
            {
                this.SetValue(PaneBackgroundProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the border brush of the pane.
        /// </summary>
        public SolidColorBrush PaneBorderBrush
        {
            get
            {
                return this.GetValue(PaneBorderBrushProperty) as SolidColorBrush;
            }
            set
            {
                this.SetValue(PaneBorderBrushProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the background color of app menu buttons.
        /// </summary>
        public SolidColorBrush AppMenuButtonBackground
        {
            get
            {
                return this.GetValue(AppMenuButtonBackgroundProperty) as SolidColorBrush;
            }
            set
            {
                this.SetValue(AppMenuButtonBackgroundProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the background color of app menu buttons that are checked.
        /// </summary>
        public SolidColorBrush AppMenuButtonCheckedBackground
        {
            get
            {
                return this.GetValue(AppMenuButtonCheckedBackgroundProperty) as SolidColorBrush;
            }
            set
            {
                this.SetValue(AppMenuButtonCheckedBackgroundProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the background color of app menu buttons that are pressed down.
        /// </summary>
        public SolidColorBrush AppMenuButtonPressedBackground
        {
            get
            {
                return this.GetValue(AppMenuButtonPressedBackgroundProperty) as SolidColorBrush;
            }
            set
            {
                this.SetValue(AppMenuButtonPressedBackgroundProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the background color of app menu buttons that are hovered over.
        /// </summary>
        public SolidColorBrush AppMenuButtonHoverBackground
        {
            get
            {
                return this.GetValue(AppMenuButtonHoverBackgroundProperty) as SolidColorBrush;
            }
            set
            {
                this.SetValue(AppMenuButtonHoverBackgroundProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the foreground color of app menu buttons.
        /// </summary>
        public SolidColorBrush AppMenuButtonForeground
        {
            get
            {
                return this.GetValue(AppMenuButtonForegroundProperty) as SolidColorBrush;
            }
            set
            {
                this.SetValue(AppMenuButtonForegroundProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the foreground color of app menu buttons that are checked.
        /// </summary>
        public SolidColorBrush AppMenuButtonCheckedForeground
        {
            get
            {
                return this.GetValue(AppMenuButtonCheckedForegroundProperty) as SolidColorBrush;
            }
            set
            {
                this.SetValue(AppMenuButtonCheckedForegroundProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the secondary button separator.
        /// </summary>
        public SolidColorBrush SecondarySeparatorColor
        {
            get
            {
                return this.GetValue(SecondarySeparatorColorProperty) as SolidColorBrush;
            }
            set
            {
                this.SetValue(SecondarySeparatorColorProperty, value);
            }
        }

        /// <summary>
        /// Refreshes the styles of the <see cref="AppMenu"/>
        /// </summary>
        /// <param name="color">
        /// The color to update with.
        /// </param>
        public void RefreshStyles(Color? color)
        {
            if (color == null)
            {
                return;
            }

            this.PaneButtonBackground = color.Value.ToSolidColorBrush();
            this.PaneButtonForeground = Colors.White.ToSolidColorBrush();
            this.PaneBackground = Colors.Black.ToSolidColorBrush();
            this.AppMenuButtonBackground = Colors.Transparent.ToSolidColorBrush();
            this.AppMenuButtonForeground = Colors.White.ToSolidColorBrush();
            this.AppMenuButtonCheckedForeground = Colors.White.ToSolidColorBrush();
            this.AppMenuButtonCheckedBackground = color.Value.Darken(40).ToSolidColorBrush();
            this.AppMenuButtonPressedBackground = color.Value.Darken(30).ToSolidColorBrush();
            this.AppMenuButtonHoverBackground = color.Value.Lighten(30).ToSolidColorBrush();
            this.SecondarySeparatorColor = this.PaneBorderBrush = Colors.Gray.ToSolidColorBrush();
        }
    }
}