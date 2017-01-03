namespace WinUX.Xaml.Converters
{
    using System;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Defines a value converter for converting a <see cref="DateTime"/> to a <see cref="DateTimeOffset"/>.
    /// </summary>
    public sealed class DateTimeToDateTimeOffsetConverter : IValueConverter
    {
        /// <summary>
        /// Converts a <see cref="DateTime"/> value to a <see cref="DateTimeOffset"/> value.
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
        /// Returns the converted <see cref="DateTimeOffset"/>.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var val = value as DateTime?;
            return val == null ? DateTimeOffset.MinValue : new DateTimeOffset(val.Value);
        }

        /// <summary>
        /// Converts a <see cref="DateTimeOffset"/> value back to a <see cref="DateTime"/> value.
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
        /// Returns the reverted <see cref="DateTime"/>.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (!(value is DateTimeOffset)) return DependencyProperty.UnsetValue;

            var temp = (DateTimeOffset)value;
            return temp.Date;
        }
    }
}