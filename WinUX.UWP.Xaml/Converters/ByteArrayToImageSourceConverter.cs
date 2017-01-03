namespace WinUX.Xaml.Converters
{
    using System;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Media.Imaging;

    /// <summary>
    /// Defines a value converter for converting a <see cref="byte"/> array into an <see cref="BitmapSource"/> value.
    /// </summary>
    public sealed class ByteArrayToImageSourceConverter : IValueConverter
    {
        /// <summary>
        /// Converts a <see cref="byte"/> array value to a <see cref="BitmapSource"/> value.
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
        /// Returns the converted <see cref="BitmapSource"/>.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var byteArray = value as byte[];
            return byteArray?.ToBitmapSource();
        }

        /// <summary>
        /// Convert back is not supported by the <see cref="ByteArrayToImageSourceConverter"/>.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}