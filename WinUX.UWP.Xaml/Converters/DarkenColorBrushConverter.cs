namespace WinUX.Xaml.Converters
{
    using System;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Media;

    using WinUX.Common;
    using WinUX.Extensions;

    /// <summary>
    /// Defines a value converter for darkening a <see cref="SolidColorBrush"/>.
    /// </summary>
    public sealed class DarkenColorBrushConverter : IValueConverter
    {
        /// <summary>
        /// Converts a <see cref="SolidColorBrush"/> to a darker <see cref="SolidColorBrush"/> by the specified parameter amount.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="targetType">
        /// The target Type.
        /// </param>
        /// <param name="parameter">
        /// The amount to darken by. Defaults to 30.
        /// </param>
        /// <param name="language">
        /// The language.
        /// </param>
        /// <returns>
        /// Returns the darkened <see cref="SolidColorBrush"/>.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var brush = value as SolidColorBrush;
            if (brush == null)
            {
                return DependencyProperty.UnsetValue;
            }

            var darkenAmount = ParseHelper.SafeParseFloat(parameter);
            if (darkenAmount.IsZero())
            {
                // Apply darken default.
                darkenAmount = 30;
            }

            var darkerColor = brush.Color.Darken(darkenAmount);

            return new SolidColorBrush(darkerColor);
        }

        /// <summary>
        /// Convert back is not supported by the <see cref="DarkenColorBrushConverter"/>.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}