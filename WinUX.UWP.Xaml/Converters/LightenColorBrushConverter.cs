namespace WinUX.Xaml.Converters
{
    using System;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Media;

    using WinUX.Common;

    /// <summary>
    /// Defines a value converter for lightening a <see cref="SolidColorBrush"/>.
    /// </summary>
    public sealed class LightenColorBrushConverter : IValueConverter
    {
        /// <summary>
        /// Converts a <see cref="SolidColorBrush"/> to a lighter <see cref="SolidColorBrush"/> by the specified parameter amount.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="targetType">
        /// The target Type.
        /// </param>
        /// <param name="parameter">
        /// The amount to lighten by. Defaults to 30.
        /// </param>
        /// <param name="language">
        /// The language.
        /// </param>
        /// <returns>
        /// Returns the lightened <see cref="SolidColorBrush"/>.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var brush = value as SolidColorBrush;
            if (brush == null)
            {
                return DependencyProperty.UnsetValue;
            }

            var lightenAmount = ParseHelper.SafeParseFloat(parameter);
            if (lightenAmount.IsZero())
            {
                // Apply lighten default.
                lightenAmount = 30;
            }

            var lighterColor = brush.Color.Lighten(lightenAmount);

            return new SolidColorBrush(lighterColor);
        }

        /// <summary>
        /// Convert back is not supported by the <see cref="LightenColorBrushConverter"/>.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}