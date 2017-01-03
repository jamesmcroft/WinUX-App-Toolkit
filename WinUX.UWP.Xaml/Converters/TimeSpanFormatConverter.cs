namespace WinUX.Xaml.Converters
{
    using System;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Defines a value converter for converting a <see cref="TimeSpan"/> to a formatted <see cref="string"/>.
    /// </summary>
    public sealed class TimeSpanFormatConverter : IValueConverter
    {
        /// <summary>
        /// Converts a <see cref="TimeSpan"/> value to a <see cref="string"/> value.
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
        /// Returns the <see cref="TimeSpan"/> as a formatted <see cref="string"/>.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var val = value as TimeSpan?;
            if (val == null) return DependencyProperty.UnsetValue;

            var param = parameter as string;
            return param == null ? string.Empty : (val.Value == TimeSpan.MinValue ? null : val.Value.ToString(param));
        }

        /// <summary>
        /// Convert back is not supported by the <see cref="TimeSpanFormatConverter"/>.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}