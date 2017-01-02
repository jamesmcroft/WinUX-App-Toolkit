namespace WinUX.UWP.Extensions
{
    using System;
    using System.Text;
    using System.Threading.Tasks;

    using Windows.Storage.Streams;

    /// <summary>
    /// Defines a collection of extensions for Streams.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Reads the contents of the specified stream as a string using ASCII encoding.
        /// </summary>
        /// <param name="stream">
        /// The stream to read from.
        /// </param>
        /// <returns>Stream content.</returns>
        public static Task<string> ReadAsStringAsync(this IRandomAccessStream stream)
        {
            return ReadAsStringAsync(stream, Encoding.ASCII);
        }

        /// <summary>
        /// Reads the contents of the specified stream as a string.
        /// </summary>
        /// <param name="stream">
        /// The stream to read from.
        /// </param>
        /// <param name="encoding">
        /// The encoding to use. Defaults to Encoding.ASCII.
        /// </param>
        /// <returns>Stream content.</returns>
        public static async Task<string> ReadAsStringAsync(this IRandomAccessStream stream, Encoding encoding)
        {
            var reader = new DataReader(stream.GetInputStreamAt(0));
            await reader.LoadAsync((uint)stream.Size);

            var bytes = new byte[stream.Size];
            reader.ReadBytes(bytes);

            if (encoding == null)
            {
                encoding = Encoding.ASCII;
            }

            return encoding.GetString(bytes);
        }
    }
}