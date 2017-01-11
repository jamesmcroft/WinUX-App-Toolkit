namespace WinUX.Xaml.Converters
{
    using System;

    using Windows.UI.Xaml.Data;

    using WinUX.Common;

    /// <summary>
    /// Defines a value converter for converting a seconds value as a double to a TimeSpan.
    /// </summary>
    public class DoubleToTimeSpanConverter : IValueConverter
    {
        /// <summary>
        /// Converts a double value into a TimeSpan value.
        /// </summary>
        /// <param name="value">
        /// A <see cref="double"/> value.
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
        /// Returns the TimeSpan representation of the double value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided value is null.
        /// </exception>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var val = ParseHelper.SafeParseDouble(value.ToString());

            return TimeSpan.FromSeconds(val);
        }

        /// <summary>
        /// Converts a TimeSpan value back to a double value.
        /// </summary>
        /// <param name="value">
        /// A <see cref="TimeSpan"/> value.
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
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided value is null.
        /// </exception>
        /// <returns>
        /// Returns the double representation of the TimeSpan value in seconds.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var timeSpan = value as TimeSpan? ?? TimeSpan.Zero;
            return timeSpan.TotalSeconds;
        }
    }
}