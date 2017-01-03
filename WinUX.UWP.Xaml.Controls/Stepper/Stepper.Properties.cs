namespace WinUX.Xaml.Controls
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media;

    /// <summary>
    /// Defines the properties for the <see cref="Stepper"/> control.
    /// </summary>
    public partial class Stepper
    {
        /// <summary>
        /// Defines the dependency property for the <see cref="MinimumValue"/>.
        /// </summary>
        public static readonly DependencyProperty MinimumValueProperty =
            DependencyProperty.Register(
                nameof(MinimumValue),
                typeof(double),
                typeof(Stepper),
                new PropertyMetadata(0.0, (d, e) => ((Stepper)d).Update()));

        /// <summary>
        /// Defines the dependency property for the <see cref="MaximumValue"/>.
        /// </summary>
        public static readonly DependencyProperty MaximumValueProperty =
            DependencyProperty.Register(
                nameof(MaximumValue),
                typeof(double),
                typeof(Stepper),
                new PropertyMetadata(double.MaxValue, (d, e) => ((Stepper)d).Update()));

        /// <summary>
        /// Defines the dependency property for the <see cref="Value"/>.
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value),
            typeof(double),
            typeof(Stepper),
            new PropertyMetadata(0.0, (d, e) => ((Stepper)d).Update()));

        /// <summary>
        /// Defines the dependency property for the <see cref="StepValue"/>.
        /// </summary>
        public static readonly DependencyProperty StepValueProperty = DependencyProperty.Register(
            nameof(StepValue),
            typeof(double),
            typeof(Stepper),
            new PropertyMetadata(1.0, (d, e) => ((Stepper)d).Update()));

        /// <summary>
        /// Defines the dependency property for the <see cref="ValueFormat"/>.
        /// </summary>
        public static readonly DependencyProperty ValueFormatProperty = DependencyProperty.Register(
            nameof(ValueFormat),
            typeof(string),
            typeof(Stepper),
            new PropertyMetadata(string.Empty, (d, e) => ((Stepper)d).Update()));

        /// <summary>
        /// Defines the dependency property for the <see cref="Autorepeat"/>.
        /// </summary>
        public static readonly DependencyProperty AutorepeatProperty = DependencyProperty.Register(
            nameof(Autorepeat),
            typeof(bool),
            typeof(Stepper),
            new PropertyMetadata(false, (d, e) => ((Stepper)d).UpdateForAutorepeat((bool)e.NewValue)));

        /// <summary>
        /// Defines the dependency property for the <see cref="Wraps"/>.
        /// </summary>
        public static readonly DependencyProperty WrapsProperty = DependencyProperty.Register(
            nameof(Wraps),
            typeof(bool),
            typeof(Stepper),
            new PropertyMetadata(false, (d, e) => ((Stepper)d).Update()));

        /// <summary>
        /// Defines the dependency property for the <see cref="ButtonForeground"/>.
        /// </summary>
        public static readonly DependencyProperty ButtonForegroundProperty =
            DependencyProperty.Register(
                nameof(ButtonForeground),
                typeof(Brush),
                typeof(Stepper),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the button foreground color brush.
        /// </summary>
        public Brush ButtonForeground
        {
            get
            {
                return (Brush)this.GetValue(ButtonForegroundProperty);
            }
            set
            {
                this.SetValue(ButtonForegroundProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the value wraps around when hitting the maximum/minimum.
        /// </summary>
        public bool Wraps
        {
            get
            {
                return (bool)this.GetValue(WrapsProperty);
            }
            set
            {
                this.SetValue(WrapsProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to repeat the step when holding the button.
        /// </summary>
        public bool Autorepeat
        {
            get
            {
                return (bool)this.GetValue(AutorepeatProperty);
            }
            set
            {
                this.SetValue(AutorepeatProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the double string formatting for the value.
        /// </summary>
        public string ValueFormat
        {
            get
            {
                return (string)this.GetValue(ValueFormatProperty);
            }
            set
            {
                this.SetValue(ValueFormatProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the step, or increment, value.
        /// </summary>
        public double StepValue
        {
            get
            {
                return (double)this.GetValue(StepValueProperty);
            }
            set
            {
                this.SetValue(StepValueProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the current value.
        /// </summary>
        public double Value
        {
            get
            {
                return (double)this.GetValue(ValueProperty);
            }
            set
            {
                this.SetValue(ValueProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the minimum possible value.
        /// </summary>
        public double MinimumValue
        {
            get
            {
                return (double)this.GetValue(MinimumValueProperty);
            }
            set
            {
                this.SetValue(MinimumValueProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum possible value.
        /// </summary>
        public double MaximumValue
        {
            get
            {
                return (double)this.GetValue(MaximumValueProperty);
            }
            set
            {
                this.SetValue(MaximumValueProperty, value);
            }
        }

        private Button AddButton { get; set; }

        private TextBlock ValueTextBlock { get; set; }

        private Button SubtractButton { get; set; }
    }
}