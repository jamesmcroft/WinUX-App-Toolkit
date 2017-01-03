namespace WinUX.Xaml.Converters
{
    using System;

    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Defines a value converter for inverting a <see cref="bool"/>.
    /// </summary>
    public sealed class InverseBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Inverts a <see cref="bool"/> value.
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
        /// Returns true if false; else true.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return !(value as bool?) ?? value;
        }

        /// <summary>
        /// Inverts an inverted <see cref="bool"/> value
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
        /// Returns true if false; else true.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return !(value as bool?) ?? value;
        }
    }
}