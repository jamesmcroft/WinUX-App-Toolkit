namespace WinUX.Xaml.Converters.Collections
{
    using System;
    using System.Globalization;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Defines a basic model to represent a ValueConverter.
    /// </summary>
    public sealed class ValueConverter : DependencyObject
    {
        /// <summary>
        /// Defines the dependency property for <see cref="Converter"/>.
        /// </summary>
        public static readonly DependencyProperty ConverterProperty = DependencyProperty.Register(
            nameof(Converter),
            typeof(IValueConverter),
            typeof(ValueConverter),
            new PropertyMetadata(null));

        /// <summary>
        /// Defines the dependency property for <see cref="ConverterParameter"/>.
        /// </summary>
        public static readonly DependencyProperty ConverterParameterProperty =
            DependencyProperty.Register(
                nameof(ConverterParameter),
                typeof(object),
                typeof(ValueConverter),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the parameter to pass to the converter.
        /// </summary>
        public object ConverterParameter
        {
            get
            {
                return this.GetValue(ConverterParameterProperty);
            }
            set
            {
                this.SetValue(ConverterParameterProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the converter to execute.
        /// </summary>
        public IValueConverter Converter
        {
            get
            {
                return (IValueConverter)this.GetValue(ConverterProperty);
            }
            set
            {
                this.SetValue(ConverterProperty, value);
            }
        }

        /// <summary>
        /// Converts the specified value using the associated <see cref="Converter"/>.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <param name="targetType">
        /// The target type.
        /// </param>
        /// <returns>
        /// Returns the converted object.
        /// </returns>
        public object Convert(object value, Type targetType)
        {
            return this.Converter?.Convert(
                value,
                targetType,
                this.ConverterParameter,
                CultureInfo.CurrentCulture.ToString());
        }

        /// <summary>
        /// Converts a converted object back to the original object using the associated <see cref="Converter"/>.
        /// </summary>
        /// <param name="value">
        /// The converted value.
        /// </param>
        /// <param name="targetType">
        /// The target type.
        /// </param>
        /// <returns>
        /// Returns the reverted object.
        /// </returns>
        public object ConvertBack(object value, Type targetType)
        {
            return this.Converter?.ConvertBack(
                value,
                targetType,
                this.ConverterParameter,
                CultureInfo.CurrentCulture.ToString());
        }
    }
}