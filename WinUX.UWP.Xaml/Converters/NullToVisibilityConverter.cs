namespace WinUX.Xaml.Converters
{
    using System;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Defines a value converter for checking whether a value is null and returns a <see cref="Visibility"/>.
    /// </summary>
    public sealed class NullToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Checks whether the specified <see cref="value"/> is null returning a <see cref="Visibility"/> representation.
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
        /// Returns Visible if null; else Collapsed.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value == null ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Convert back is not supported by the <see cref="NullToVisibilityConverter"/>.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}