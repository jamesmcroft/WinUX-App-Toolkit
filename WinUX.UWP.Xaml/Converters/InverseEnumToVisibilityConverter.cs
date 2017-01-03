namespace WinUX.Xaml.Converters
{
    using System;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Defines a value converter for inverting the conversion of an <see cref="Enum"/> value to a <see cref="Visibility"/> if it matches.
    /// </summary>
    public sealed class InverseEnumToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts an <see cref="Enum"/> value to a <see cref="Visibility"/> value if it matches.
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
        /// Returns Collapsed if matches; else Visible.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var parameterString = parameter as string;
            if (parameterString == null) return Visibility.Collapsed;

            if (value is string)
            {
                return parameter.Equals(value) ? Visibility.Collapsed : Visibility.Visible;
            }

            if (Enum.IsDefined(value.GetType(), value) == false) return Visibility.Collapsed;

            var parameterValue = Enum.Parse(value.GetType(), parameterString);
            return parameterValue.Equals(value) ? Visibility.Collapsed : Visibility.Visible;
        }

        /// <summary>
        /// Convert back is not supported by the <see cref="InverseEnumToVisibilityConverter"/>.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}