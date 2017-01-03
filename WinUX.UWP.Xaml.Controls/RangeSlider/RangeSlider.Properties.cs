namespace WinUX.Xaml.Controls
{
    using Windows.UI.Xaml;

    /// <summary>
    /// Defines the properties for the <see cref="RangeSlider"/> control.
    /// </summary>
    public partial class RangeSlider
    {
        /// <summary>
        /// Defines the dependency property for the <see cref="Minimum"/>.
        /// </summary>
        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register(
            nameof(Minimum),
            typeof(double),
            typeof(RangeSlider),
            new PropertyMetadata(
                0.0,
                (d, e) => ((RangeSlider)d).OnMinimumChanged((double)e.OldValue, (double)e.NewValue)));
        
        /// <summary>
        /// Defines the dependency property for the <see cref="Maximum"/>.
        /// </summary>
        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(
            nameof(Maximum),
            typeof(double),
            typeof(RangeSlider),
            new PropertyMetadata(
                1.0,
                (d, e) => ((RangeSlider)d).OnMaximumChanged((double)e.OldValue, (double)e.NewValue)));

        /// <summary>
        /// Defines the dependency property for the <see cref="SelectedMinimum"/>.
        /// </summary>
        public static readonly DependencyProperty SelectedMinimumProperty =
            DependencyProperty.Register(
                nameof(SelectedMinimum),
                typeof(double),
                typeof(RangeSlider),
                new PropertyMetadata(0.0, (d, e) => ((RangeSlider)d).OnSelectedMinimumChanged((double)e.NewValue)));

        /// <summary>
        /// Defines the dependency property for the <see cref="SelectedMaximum"/>.
        /// </summary>
        public static readonly DependencyProperty SelectedMaximumProperty =
            DependencyProperty.Register(
                nameof(SelectedMaximum),
                typeof(double),
                typeof(RangeSlider),
                new PropertyMetadata(1.0, (d, e) => ((RangeSlider)d).OnSelectedMaximumChanged((double)e.NewValue)));

        /// <summary>
        /// Gets or sets the minimum acceptable value for the slider.
        /// </summary>
        public double Minimum
        {
            get
            {
                return (double)this.GetValue(MinimumProperty);
            }

            set
            {
                this.SetValue(MinimumProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum acceptable value for the slider.
        /// </summary>
        public double Maximum
        {
            get
            {
                return (double)this.GetValue(MaximumProperty);
            }

            set
            {
                this.SetValue(MaximumProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the selected minimum value.
        /// </summary>
        public double SelectedMinimum
        {
            get
            {
                return (double)this.GetValue(SelectedMinimumProperty);
            }

            set
            {
                this.SetValue(SelectedMinimumProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the selected maximum value.
        /// </summary>
        public double SelectedMaximum
        {
            get
            {
                return (double)this.GetValue(SelectedMaximumProperty);
            }

            set
            {
                this.SetValue(SelectedMaximumProperty, value);
            }
        }
    }
}