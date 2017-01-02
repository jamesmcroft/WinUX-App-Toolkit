namespace WinUX.Storage
{
    /// <summary>
    /// Defines the enumeration values for a files's size format.
    /// </summary>
    public enum FileSizeFormat
    {
        /// <summary>
        /// Byte, Default.
        /// </summary>
        Byte,

        /// <summary>
        /// Kilobyte, 1024 * Byte.
        /// </summary>
        Kilobyte,

        /// <summary>
        /// Megabyte, 1024 * Kilobyte.
        /// </summary>
        Megabyte,

        /// <summary>
        /// Gigabyte, 1024 * Megabyte.
        /// </summary>
        Gigabyte,

        /// <summary>
        /// Terabyte, 1024 * Gigabyte.
        /// </summary>
        Terabyte,

        /// <summary>
        /// Petabyte, 1024 * Terabyte.
        /// </summary>
        Petabyte
    }
}