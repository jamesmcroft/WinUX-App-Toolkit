namespace WinUX.Xaml.Converters
{
    using System;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Defines a value converter for inverting the conversion of a <see cref="string"/> to a <see cref="Visibility"/>.
    /// </summary>
    public sealed class InverseStringToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts a <see cref="string"/> value to a <see cref="Visibility"/> value.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="targetType">
        /// The target Type.
        /// </param>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <param name="language">
        /// The language.
        /// </param>
        /// <returns>
        /// Returns Visible if the string is empty or null; else Collapsed.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null) return Visibility.Visible;

            var val = value.ToString();
            return string.IsNullOrWhiteSpace(val) ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Convert back is not supported by the <see cref="InverseStringToVisibilityConverter"/>.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}