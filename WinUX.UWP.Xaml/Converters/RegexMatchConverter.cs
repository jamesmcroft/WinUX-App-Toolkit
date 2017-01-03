namespace WinUX.Xaml.Converters
{
    using System;
    using System.Text.RegularExpressions;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Defines a value converter for matching a value with a specified regular expression.
    /// </summary>
    public sealed class RegexMatchConverter : IValueConverter
    {
        /// <summary>
        /// Gets or sets the regular expression to match with.
        /// </summary>
        public string Regex { get; set; }

        /// <summary>
        /// Converts a <see cref="string"/> value to a <see cref="bool"/> value.
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
        /// Returns true if the value matches the <see cref="Regex"/>; else false.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null) return false;

            var val = value.ToString();
            if (string.IsNullOrWhiteSpace(val))
            {
                return true;
            }

            var reg = new Regex(this.Regex, RegexOptions.IgnoreCase);
            return reg.IsMatch(val);
        }

        /// <summary>
        /// Convert back is not supported by the <see cref="RegexMatchConverter"/>.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}