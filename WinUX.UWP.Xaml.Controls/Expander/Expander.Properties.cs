namespace WinUX.Xaml.Controls
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Media;

    /// <summary>
    /// Defines the properties for the <see cref="Expander"/> control.
    /// </summary>
    public partial class Expander
    {
        /// <summary>
        /// Defines the dependency property for the <see cref="Header"/>.
        /// </summary>
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            nameof(Header),
            typeof(object),
            typeof(Expander),
            new PropertyMetadata(null));

        /// <summary>
        /// Defines the dependency property for the <see cref="HeaderGlyph"/>.
        /// </summary>
        public static readonly DependencyProperty HeaderGlyphProperty = DependencyProperty.Register(
            nameof(HeaderGlyph),
            typeof(object),
            typeof(Expander),
            new PropertyMetadata(null));

        /// <summary>
        /// Defines the dependency property for the <see cref="HeaderBorderBrush"/>.
        /// </summary>
        public static readonly DependencyProperty HeaderBorderBrushProperty =
            DependencyProperty.Register(
                nameof(HeaderBorderBrush),
                typeof(Brush),
                typeof(Expander),
                new PropertyMetadata(null));

        /// <summary>
        /// Defines the dependency property for the <see cref="HeaderBackground"/>.
        /// </summary>
        public static readonly DependencyProperty HeaderBackgroundProperty =
            DependencyProperty.Register(
                nameof(HeaderBackground),
                typeof(Brush),
                typeof(Expander),
                new PropertyMetadata(null));

        /// <summary>
        /// Defines the dependency property for the <see cref="HeaderBorderThickness"/>.
        /// </summary>
        public static readonly DependencyProperty HeaderBorderThicknessProperty =
            DependencyProperty.Register(
                nameof(HeaderBorderThickness),
                typeof(Thickness),
                typeof(Expander),
                new PropertyMetadata(new Thickness(0)));

        /// <summary>
        /// Defines the dependency property for the <see cref="IsExpanded"/>.
        /// </summary>
        public static readonly DependencyProperty IsExpandedProperty = DependencyProperty.Register(
            nameof(IsExpanded),
            typeof(bool),
            typeof(Expander),
            new PropertyMetadata(
                true,
                (d, e) =>
                    {
                        var control = (Expander)d;
                        control.SetState((bool)e.NewValue, control.UseAnimations);
                    }));

        /// <summary>
        /// Defines the dependency property for the <see cref="UseAnimations"/>.
        /// </summary>
        public static readonly DependencyProperty UseAnimationsProperty =
            DependencyProperty.Register(
                nameof(UseAnimations),
                typeof(bool),
                typeof(Expander),
                new PropertyMetadata(true));

        /// <summary>
        /// Defines the dependency property for the <see cref="HeaderGlyphVisibility"/>.
        /// </summary>
        public static readonly DependencyProperty HeaderGlyphVisibilityProperty =
            DependencyProperty.Register(
                nameof(HeaderGlyphVisibility),
                typeof(Visibility),
                typeof(Expander),
                new PropertyMetadata(Visibility.Visible));

        /// <summary>
        /// Gets or sets the visibility of the header glyph.
        /// </summary>
        public Visibility HeaderGlyphVisibility
        {
            get
            {
                return (Visibility)this.GetValue(HeaderGlyphVisibilityProperty);
            }
            set
            {
                this.SetValue(HeaderGlyphVisibilityProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use animations when expanding/collapsing.
        /// </summary>
        public bool UseAnimations
        {
            get
            {
                return (bool)this.GetValue(UseAnimationsProperty);
            }
            set
            {
                this.SetValue(UseAnimationsProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        public object Header
        {
            get
            {
                return this.GetValue(HeaderProperty);
            }
            set
            {
                this.SetValue(HeaderProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the header glyph.
        /// </summary>
        public object HeaderGlyph
        {
            get
            {
                return this.GetValue(HeaderGlyphProperty);
            }
            set
            {
                this.SetValue(HeaderGlyphProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the header's border brush.
        /// </summary>
        public Brush HeaderBorderBrush
        {
            get
            {
                return (Brush)this.GetValue(HeaderBorderBrushProperty);
            }
            set
            {
                this.SetValue(HeaderBorderBrushProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the header's background brush.
        /// </summary>
        public Brush HeaderBackground
        {
            get
            {
                return (Brush)this.GetValue(HeaderBackgroundProperty);
            }
            set
            {
                this.SetValue(HeaderBackgroundProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the header's border thickness.
        /// </summary>
        public Thickness HeaderBorderThickness
        {
            get
            {
                return (Thickness)this.GetValue(HeaderBorderThicknessProperty);
            }
            set
            {
                this.SetValue(HeaderBorderThicknessProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the content is expanded.
        /// </summary>
        public bool IsExpanded
        {
            get
            {
                return (bool)this.GetValue(IsExpandedProperty);
            }
            set
            {
                this.SetValue(IsExpandedProperty, value);
            }
        }

        private UIElement HeaderContainer { get; set; }
    }
}