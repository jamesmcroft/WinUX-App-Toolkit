namespace WinUX
{
    using System;

    using WinUX.Storage;

    /// <summary>
    /// Defines a collection of extensions for files.
    /// </summary>
    public static partial class Extensions
    {
        private const double Kilobyte = 1024;

        private const double Megabyte = Kilobyte * 1024;

        private const double Gigabyte = Megabyte * 1024;

        private const double Terabyte = Gigabyte * 1024;

        private const double Petabyte = Terabyte * 1024;

        /// <summary>
        /// Converts a <see cref="double"/> byte file size to another format.
        /// </summary>
        /// <param name="bytes">
        /// The file size as bytes.
        /// </param>
        /// <param name="format">
        /// The format to convert to.
        /// </param>
        /// <returns>
        /// Returns the converted file size.
        /// </returns>
        public static double ToFileSize(this double bytes, FileSizeFormat format)
        {
            switch (format)
            {
                case FileSizeFormat.Kilobyte:
                    return bytes / Kilobyte;
                case FileSizeFormat.Megabyte:
                    return bytes / Megabyte;
                case FileSizeFormat.Gigabyte:
                    return bytes / Gigabyte;
                case FileSizeFormat.Terabyte:
                    return bytes / Terabyte;
                case FileSizeFormat.Petabyte:
                    return bytes / Petabyte;
                default:
                    return bytes;
            }
        }

        /// <summary>
        /// Converts a <see cref="double"/> byte file size to a formatted string.
        /// </summary>
        /// <param name="bytes">
        /// The file size as bytes.
        /// </param>
        /// <param name="format">
        /// The file size format.
        /// </param>
        /// <returns>
        /// Returns the formatted file size string.
        /// </returns>
        public static string ToFileSizeString(this double bytes, FileSizeFormat format)
        {
            var roundedFileSize = Math.Round(bytes.ToFileSize(format), 2);

            switch (format)
            {
                case FileSizeFormat.Byte:
                    return $"{roundedFileSize}B";
                case FileSizeFormat.Kilobyte:
                    return $"{roundedFileSize}KB";
                case FileSizeFormat.Megabyte:
                    return $"{roundedFileSize}MB";
                case FileSizeFormat.Gigabyte:
                    return $"{roundedFileSize}GB";
                case FileSizeFormat.Terabyte:
                    return $"{roundedFileSize}TB";
                case FileSizeFormat.Petabyte:
                    return $"{roundedFileSize}PB";
            }

            return $"{bytes}B";
        }
    }
}