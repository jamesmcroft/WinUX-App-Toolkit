namespace WinUX
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices.WindowsRuntime;
    using System.Threading.Tasks;

    using Windows.Graphics.Display;
    using Windows.Graphics.Imaging;
    using Windows.Storage;
    using Windows.Storage.Streams;
    using Windows.UI;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Media.Imaging;

    /// <summary>
    /// Defines a collection of extensions for image handling.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Converts a stream representing an image into a <see cref="SoftwareBitmap"/>.
        /// </summary>
        /// <param name="stream">
        /// The image stream.
        /// </param>
        /// <returns>
        /// Returns a <see cref="SoftwareBitmap"/> of the specified stream wrapped in an await-able task.
        /// </returns>
        public static async Task<SoftwareBitmap> ToSoftwareBitmapAsync(this Stream stream)
        {
            var decoder = await BitmapDecoder.CreateAsync(stream.AsRandomAccessStream());
            var softwareBitmap = await decoder.GetSoftwareBitmapAsync();

            var result = SoftwareBitmap.Convert(
                softwareBitmap,
                BitmapPixelFormat.Bgra8,
                BitmapAlphaMode.Premultiplied);

            return result;
        }

        /// <summary>
        /// Converts a <see cref="SoftwareBitmap"/> into an <see cref="ImageSource"/>.
        /// </summary>
        /// <param name="softwareBitmap">
        /// The software bitmap.
        /// </param>
        /// <returns>
        /// Returns an <see cref="ImageSource"/> of the specified software bitmap wrapped in an await-able task.
        /// </returns>
        public static async Task<ImageSource> ToImageSourceAsync(this SoftwareBitmap softwareBitmap)
        {
            var result = new SoftwareBitmapSource();
            await result.SetBitmapAsync(softwareBitmap);
            return result;
        }

        /// <summary>
        /// Converts a <see cref="Stream"/> into an <see cref="ImageSource"/>.
        /// </summary>
        /// <param name="stream">
        /// The stream.
        /// </param>
        /// <returns>
        /// Returns an <see cref="ImageSource"/> of the specified stream wrapped in an await-able task.
        /// </returns>
        public static async Task<ImageSource> ToImageSourceAsync(this Stream stream)
        {
            var softwareBitmap = await stream.ToSoftwareBitmapAsync();
            var result = await softwareBitmap.ToImageSourceAsync();
            return result;
        }

        /// <summary>
        /// Converts an array of bytes representing an image into a <see cref="BitmapSource"/>.
        /// </summary>
        /// <param name="imageBytes">
        /// The image bytes.
        /// </param>
        /// <returns>
        /// Returns a <see cref="BitmapSource"/> of the specified bytes wrapped in an await-able task.
        /// </returns>
        public static async Task<BitmapSource> ToBitmapSourceAsync(this byte[] imageBytes)
        {
            using (var raStream = new InMemoryRandomAccessStream())
            {
                using (var writer = new DataWriter(raStream))
                {
                    // Write the bytes to the stream
                    writer.WriteBytes(imageBytes);

                    // Store the bytes to the MemoryStream
                    await writer.StoreAsync();

                    // Not necessary, but do it anyway
                    await writer.FlushAsync();

                    // Detach from the Memory stream so we don't close it
                    writer.DetachStream();
                }

                raStream.Seek(0);

                BitmapSource bitMapSource = new BitmapImage();
                bitMapSource.SetSource(raStream);

                return bitMapSource;
            }
        }

        /// <summary>
        /// Converts an array of bytes representing an image into a <see cref="BitmapSource"/>.
        /// </summary>
        /// <param name="imageBytes">
        /// The image bytes.
        /// </param>
        /// <returns>
        /// Returns a <see cref="BitmapSource"/> of the specified bytes.
        /// </returns>
        public static BitmapSource ToBitmapSource(this byte[] imageBytes)
        {
            using (var raStream = new InMemoryRandomAccessStream())
            {
                using (var writer = new DataWriter(raStream))
                {
                    // Write the bytes to the stream
                    writer.WriteBytes(imageBytes);

                    // Store the bytes to the MemoryStream
                    writer.StoreAsync().GetResults();

                    // Detach from the Memory stream so we don't close it
                    writer.DetachStream();
                }

                raStream.Seek(0);

                BitmapSource bitMapSource = new BitmapImage();
                bitMapSource.SetSource(raStream);

                return bitMapSource;
            }
        }

        /// <summary>
        /// Captures the specified <see cref="FrameworkElement"/> as an image in the specified <see cref="StorageFile"/>.
        /// </summary>
        /// <param name="element">
        /// The <see cref="FrameworkElement"/> to capture.
        /// </param>
        /// <param name="target">
        /// The target <see cref="StorageFile"/>.
        /// </param>
        /// <param name="encoderId">
        /// The ID of the encoder to use when creating the image.
        /// </param>
        /// <returns>
        /// Returns the <see cref="StorageFile"/> containing the image.
        /// </returns>
        public static async Task<StorageFile> CaptureElementAsImageAsync(
            this FrameworkElement element,
            StorageFile target,
            Guid encoderId)
        {
            if (target != null)
            {
                using (var stream = await target.OpenAsync(FileAccessMode.ReadWrite))
                {
                    var renderTargetBitmap = new RenderTargetBitmap();
                    await renderTargetBitmap.RenderAsync(element);

                    var pixels = await renderTargetBitmap.GetPixelsAsync();

                    var logicalDpi = DisplayInformation.GetForCurrentView().LogicalDpi;
                    var encoder = await BitmapEncoder.CreateAsync(encoderId, stream);
                    encoder.SetPixelData(
                        BitmapPixelFormat.Bgra8,
                        BitmapAlphaMode.Ignore,
                        (uint)renderTargetBitmap.PixelWidth,
                        (uint)renderTargetBitmap.PixelHeight,
                        logicalDpi,
                        logicalDpi,
                        pixels.ToArray());

                    await encoder.FlushAsync();
                }
            }

            return target;
        }

        /// <summary>
        /// Replaces all color in the specified <see cref="WriteableBitmap"/> with the new color.
        /// </summary>
        /// <remarks>
        /// Will not change transparent, white or black colors.
        /// </remarks>
        /// <param name="bitmap">
        /// The bitmap to change.
        /// </param>
        /// <param name="newColorHex">
        /// The new color.
        /// </param>
        public static void UpdateColorWith(this WriteableBitmap bitmap, string newColorHex)
        {
            if (bitmap == null)
            {
                throw new ArgumentNullException(nameof(bitmap));
            }

            if (string.IsNullOrWhiteSpace(newColorHex))
            {
                throw new ArgumentNullException(nameof(newColorHex));
            }

            var newColor = newColorHex.ToColor();

            bitmap.ForEach(
                (x, y, color) =>
                    {
                        if (color.IsInRange(Colors.White, 80, true) || color.IsInRange(Colors.Black, 80, true)
                            || color.IsInRange("#00000000".ToColor(), 80))
                        {
                            return color;
                        }

                        return newColor;
                    });
        }

        /// <summary>
        /// Replaces the specified color in the specified <see cref="WriteableBitmap"/> with the new color.
        /// </summary>
        /// <param name="bitmap">
        /// The bitmap to change.
        /// </param>
        /// <param name="colorToChangeHex">
        /// The color to change.
        /// </param>
        /// <param name="newColorHex">
        /// The new color.
        /// </param>
        public static void UpdateColorWith(this WriteableBitmap bitmap, string colorToChangeHex, string newColorHex)
        {
            if (bitmap == null)
            {
                throw new ArgumentNullException(nameof(bitmap));
            }

            if (string.IsNullOrWhiteSpace(colorToChangeHex))
            {
                throw new ArgumentNullException(nameof(colorToChangeHex));
            }

            if (string.IsNullOrWhiteSpace(newColorHex))
            {
                throw new ArgumentNullException(nameof(newColorHex));
            }

            var expectedColor = colorToChangeHex.ToColor();
            var newColor = newColorHex.ToColor();

            bitmap.ForEach((x, y, color) => color == expectedColor ? newColor : color);
        }
    }
}