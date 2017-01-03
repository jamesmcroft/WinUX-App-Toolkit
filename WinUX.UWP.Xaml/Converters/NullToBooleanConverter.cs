namespace WinUX.Xaml.Converters
{
    using System;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Defines a value converter for checking whether a value is null.
    /// </summary>
    public sealed class NullToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Checks whether the specified <see cref="value"/> is null.
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
        /// Returns true if null; else false.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value == null;
        }

        /// <summary>
        /// Convert back is not supported by the <see cref="NullToBooleanConverter"/>.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}