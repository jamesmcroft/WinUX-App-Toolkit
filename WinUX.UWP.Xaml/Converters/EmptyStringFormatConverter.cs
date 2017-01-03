namespace WinUX.Xaml.Converters
{
    using System;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Defines a value converter for providing a value if a <see cref="string"/> is empty.
    /// </summary>
    public sealed class EmptyStringFormatConverter : DependencyObject, IValueConverter
    {
        /// <summary>
        /// Defines the dependency property for <see cref="EmptyStringValue"/>.
        /// </summary>
        public static readonly DependencyProperty EmptyStringValueProperty =
            DependencyProperty.Register(
                nameof(EmptyStringValue),
                typeof(string),
                typeof(EmptyStringFormatConverter),
                new PropertyMetadata("No value"));

        /// <summary>
        /// Gets or sets the value to show when the value is empty.
        /// </summary>
        public string EmptyStringValue
        {
            get
            {
                return (string)this.GetValue(EmptyStringValueProperty);
            }
            set
            {
                this.SetValue(EmptyStringValueProperty, value);
            }
        }

        /// <summary>
        /// Checks whether the specified <see cref="value"/> is empty or null to return the <see cref="EmptyStringValue"/>.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="targetType">
        /// The target type.
        /// </param>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <param name="language">
        /// The language.
        /// </param>
        /// <returns>
        /// Returns the <see cref="EmptyStringValue"/> if the <see cref="value"/> is empty.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return this.EmptyStringValue;
            }

            var val = value.ToString();

            return string.IsNullOrWhiteSpace(val) ? this.EmptyStringValue : val;
        }

        /// <summary>
        /// Convert back is not supported by the <see cref="EmptyStringFormatConverter"/>.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}