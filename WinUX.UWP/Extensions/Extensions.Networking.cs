namespace WinUX
{
    using System;
    using System.Threading.Tasks;

    using Windows.Storage;
    using Windows.Storage.Streams;
    using Windows.Web.Http;

    /// <summary>
    /// Defines a collection of extensions for code regarding networking.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Gets the response content returned by a HTTP request.
        /// </summary>
        /// <param name="uri">
        /// The URI to request..
        /// </param>
        /// <returns>
        /// Returns the <see cref="IHttpContent"/> if returned; else null.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the specified Uri is null.
        /// </exception>
        public static async Task<IHttpContent> GetHttpContentAsync(this Uri uri)
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead))
                {
                    return !response.IsSuccessStatusCode ? null : response.Content;
                }
            }
        }

        /// <summary>
        /// Gets the response stream returned by a HTTP request.
        /// </summary>
        /// <param name="uri">
        /// The URI to request.
        /// </param>
        /// <returns>
        /// Returns the response stream.
        /// </returns>
        public static async Task<IRandomAccessStream> GetHttpResponseStreamAsync(this Uri uri)
        {
            var content = await uri.GetHttpContentAsync();

            if (content == null)
            {
                return null;
            }

            var outputStream = new InMemoryRandomAccessStream();

            using (content)
            {
                await content.WriteToStreamAsync(outputStream);

                outputStream.Seek(0);

                return outputStream;
            }
        }

        /// <summary>
        /// Gets the response stream returned by a HTTP request and saves it to a <see cref="StorageFile"/>.
        /// </summary>
        /// <param name="uri">
        /// The URI to request.
        /// </param>
        /// <param name="file">
        /// The <see cref="StorageFile"/> to store the response in.
        /// </param>
        /// <returns>
        /// Returns true if the request was successful; else false.
        /// </returns>
        public static async Task<bool> DownloadHttpResponseStreamToFileAsync(this Uri uri, StorageFile file)
        {
            var content = await uri.GetHttpContentAsync();

            if (content == null)
            {
                return false;
            }

            using (content)
            {
                using (var fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    await content.WriteToStreamAsync(fileStream);
                }
            }

            return true;
        }
    }
}