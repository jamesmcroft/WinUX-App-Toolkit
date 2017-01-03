namespace WinUX.Xaml.Converters
{
    using System;

    using Windows.UI.Xaml.Data;

    using WinUX.Common;

    /// <summary>
    /// Defines a value converter for converting a <see cref="DateTime"/> to a formatted <see cref="string"/>.
    /// </summary>
    public sealed class DateTimeFormatConverter : IValueConverter
    {
        /// <summary>
        /// Converts a <see cref="DateTime"/> value to a <see cref="string"/> value.
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
        /// Returns the <see cref="DateTime"/> as a formatted <see cref="string"/>.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var val = value as DateTime?;
            if (val == null) return string.Empty;

            var param = parameter as string;
            return param == null ? string.Empty : (val.Value == DateTime.MinValue ? null : val.Value.ToString(param));
        }

        /// <summary>
        /// Converts a <see cref="string"/> value to a <see cref="DateTime"/> value.
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
        /// Returns the parsed <see cref="DateTime"/>.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var dateTime = ParseHelper.SafeParseDateTime(value);
            return dateTime;
        }
    }
}