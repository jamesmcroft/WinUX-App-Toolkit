namespace WinUX.UWP.Networking.Requests.Streams
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Windows.Web.Http;

    using WinUX.Networking.Requests;

    /// <summary>
    /// Defines a network request for a GET call with a data stream response.
    /// </summary>
    public sealed class StreamGetNetworkRequest : NetworkRequest
    {
        private readonly HttpClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamGetNetworkRequest"/> class.
        /// </summary>
        /// <param name="client">
        /// The <see cref="HttpClient"/> for executing the request.
        /// </param>
        /// <param name="url">
        /// The URL for the request.
        /// </param>
        public StreamGetNetworkRequest(HttpClient client, string url)
            : this(client, url, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamGetNetworkRequest"/> class.
        /// </summary>
        /// <param name="client">
        /// The <see cref="HttpClient"/> for executing the request.
        /// </param>
        /// <param name="url">
        /// The URL for the request.
        /// </param>
        /// <param name="headers">
        /// The additional headers.
        /// </param>
        public StreamGetNetworkRequest(HttpClient client, string url, Dictionary<string, string> headers)
            : base(url, headers)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            this.client = client;
        }

        /// <inheritdoc />
        public override async Task<TResponse> ExecuteAsync<TResponse>(CancellationTokenSource cts = null)
        {
            return (TResponse)await this.GetStreamResponse(cts);
        }

        /// <inheritdoc />
        public override async Task<object> ExecuteAsync(Type expectedResponse, CancellationTokenSource cts = null)
        {
            return await this.GetStreamResponse(cts);
        }

        private async Task<object> GetStreamResponse(CancellationTokenSource cts = null)
        {
            if (this.client == null)
            {
                throw new InvalidOperationException(
                    "No HttpClient has been specified for executing the network request.");
            }

            if (string.IsNullOrWhiteSpace(this.Url))
            {
                throw new InvalidOperationException("No URL has been specified for executing the network request.");
            }

            var uri = new Uri(this.Url);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            if (this.Headers != null)
            {
                foreach (var header in this.Headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            var response = cts == null
                               ? await this.client.SendRequestAsync(request, HttpCompletionOption.ResponseHeadersRead)
                               : await this.client.SendRequestAsync(request, HttpCompletionOption.ResponseHeadersRead)
                                     .AsTask(cts.Token);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsInputStreamAsync();
        }
    }
}