namespace WinUX.Xaml.Converters
{
    using System;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Defines a value converter for inverting the conversion of a <see cref="bool"/> to a <see cref="Visibility"/>.
    /// </summary>
    public sealed class InverseBooleanToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts a <see cref="bool"/> value to a <see cref="Visibility"/> value.
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
        /// Returns Visible if false; else Collapsed.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var val = value as bool?;
            return val == null ? Visibility.Visible : (val.Value ? Visibility.Collapsed : Visibility.Visible);
        }

        /// <summary>
        /// Converts a <see cref="Visibility"/> value to a <see cref="bool"/> value.
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
        /// Returns true if Collapsed; else false.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var val = value as Visibility?;
            return val != null && val.Value == Visibility.Collapsed;
        }
    }
}