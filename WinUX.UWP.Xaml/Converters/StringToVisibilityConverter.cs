namespace WinUX.Xaml.Converters
{
    using System;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Defines a value converter for converting a <see cref="string"/> to a <see cref="Visibility"/>.
    /// </summary>
    public sealed class StringToVisibilityConverter : IValueConverter
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
        /// Returns Collapsed if the string is empty or null; else Visible.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null) return Visibility.Collapsed;

            var val = value.ToString();
            return string.IsNullOrWhiteSpace(val) ? Visibility.Collapsed : Visibility.Visible;
        }

        /// <summary>
        /// Convert back is not supported by the <see cref="StringToVisibilityConverter"/>.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}