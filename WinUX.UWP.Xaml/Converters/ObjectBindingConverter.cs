namespace WinUX.Xaml.Converters
{
    using System;

    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Defines a value converter that is required for x:Bind scenarios where the object cannot be bound without a converter.
    /// </summary>
    public sealed class ObjectBindingConverter : IValueConverter
    {
        /// <summary>
        /// Returns the specified <see cref="value"/>.
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
        /// Returns the <see cref="value"/>.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value;
        }

        /// <summary>
        /// Returns the specified <see cref="value"/>.
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
        /// Returns the <see cref="value"/>.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}