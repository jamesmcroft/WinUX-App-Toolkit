namespace WinUX.Xaml.Converters
{
    using System;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    using WinUX.Common;

    /// <summary>
    /// Defines a value converter for converting a <see cref="double"/> to a formatted <see cref="string"/>.
    /// </summary>
    public sealed class DoubleFormatConverter : IValueConverter
    {
        /// <summary>
        /// Converts a <see cref="double"/> value to a <see cref="string"/> value.
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
        /// Returns the <see cref="double"/> as a formatted <see cref="string"/>.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null) return DependencyProperty.UnsetValue;
            if (parameter == null) return value.ToString();

            var formatter = parameter.ToString();

            var d = ParseHelper.SafeParseDouble(value);

            return d.ToString(formatter);
        }

        /// <summary>
        /// Convert back is not supported by the <see cref="DoubleFormatConverter"/>.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}