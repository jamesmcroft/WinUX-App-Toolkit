namespace WinUX.Xaml.Converters
{
    using System;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Defines a value converter for converting an <see cref="Enum"/> value to a <see cref="bool"/> if matches.
    /// </summary>
    public sealed class EnumToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Converts an <see cref="Enum"/> value to a <see cref="bool"/> value if it matches the parameter.
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
        /// Returns true if matches; else false.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var parameterString = parameter as string;
            if (parameterString == null) return false;

            if (value is string)
            {
                return parameter.Equals(value);
            }

            if (Enum.IsDefined(value.GetType(), value) == false) return false;

            var parameterValue = Enum.Parse(value.GetType(), parameterString);
            return parameterValue.Equals(value);
        }

        /// <summary>
        /// Convert back is not supported by the <see cref="EnumToBooleanConverter"/>.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}