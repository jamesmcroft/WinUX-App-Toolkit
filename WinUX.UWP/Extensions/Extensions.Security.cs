namespace WinUX.UWP.Extensions
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using Windows.Security.Cryptography.Core;
    using Windows.Storage.Streams;

    /// <summary>
    /// Defines a collection of extensions for code regarding security.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Appends a stream to the specified cryptographic hash.
        /// </summary>
        /// <param name="hash">
        /// The <see cref="CryptographicHash"/>.
        /// </param>
        /// <param name="stream">
        /// The stream to append.
        /// </param>
        /// <returns>An await-able task</returns>
        public static async Task AppendWithStreamAsync(this CryptographicHash hash, Stream stream)
        {
            uint size = 1024 * 1024 * 5;
            await AppendWithStreamAsync(hash, stream, size);
        }

        /// <summary>
        /// Appends a stream to the specified cryptographic hash.
        /// </summary>
        /// <param name="hash">
        /// The <see cref="CryptographicHash"/>.
        /// </param>
        /// <param name="stream">
        /// The stream to append.
        /// </param>
        /// <param name="bufferSize">
        /// The buffer size.
        /// </param>
        /// <returns>
        /// An await-able task
        /// </returns>
        public static async Task AppendWithStreamAsync(this CryptographicHash hash, Stream stream, uint bufferSize)
        {
            var currentBuffer = new Windows.Storage.Streams.Buffer(bufferSize);

            using (var inputStream = stream.AsInputStream())
            {
                do
                {
                    await inputStream.ReadAsync(currentBuffer, bufferSize, InputStreamOptions.None);
                    hash.Append(currentBuffer);
                }
                while (currentBuffer.Length > 0);
            }
        }
    }
}