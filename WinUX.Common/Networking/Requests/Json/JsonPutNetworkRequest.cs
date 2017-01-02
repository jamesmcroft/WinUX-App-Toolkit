namespace WinUX.Networking.Requests.Json
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using WinUX.Data.Serialization;

    /// <summary>
    /// Defines a network request for a PUT call with a JSON response.
    /// </summary>
    public sealed class JsonPutNetworkRequest : NetworkRequest
    {
        private readonly HttpClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonPutNetworkRequest"/> class.
        /// </summary>
        /// <param name="client">
        /// The <see cref="HttpClient"/> for executing the request.
        /// </param>
        /// <param name="url">
        /// The URL for the request.
        /// </param>
        public JsonPutNetworkRequest(HttpClient client, string url)
            : this(client, url, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonPutNetworkRequest"/> class.
        /// </summary>
        /// <param name="client">
        /// The <see cref="HttpClient"/> for executing the request.
        /// </param>
        /// <param name="url">
        /// The URL for the request.
        /// </param>
        /// <param name="jsonData">
        /// The JSON data to put.
        /// </param>
        public JsonPutNetworkRequest(HttpClient client, string url, string jsonData)
            : this(client, url, jsonData, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonPutNetworkRequest"/> class.
        /// </summary>
        /// <param name="client">
        /// The <see cref="HttpClient"/> for executing the request.
        /// </param>
        /// <param name="url">
        /// The URL for the request.
        /// </param>
        /// <param name="jsonData">
        /// The JSON data to put.
        /// </param>
        /// <param name="headers">
        /// The additional headers.
        /// </param>
        public JsonPutNetworkRequest(HttpClient client, string url, string jsonData, Dictionary<string, string> headers)
            : base(url, headers)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            this.client = client;
            this.Data = jsonData;
        }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public string Data { get; set; }

        /// <inheritdoc />
        public override async Task<TResponse> ExecuteAsync<TResponse>(CancellationTokenSource cts = null)
        {
            var json = await this.GetJsonResponse(cts);
            return SerializationService.Json.Deserialize<TResponse>(json);
        }

        /// <inheritdoc />
        public override async Task<object> ExecuteAsync(Type expectedResponse, CancellationTokenSource cts = null)
        {
            var json = await this.GetJsonResponse(cts);
            return SerializationService.Json.Deserialize(json, expectedResponse);
        }

        private async Task<string> GetJsonResponse(CancellationTokenSource cts = null)
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

            var request = new HttpRequestMessage(HttpMethod.Put, uri)
                              {
                                  Content =
                                      new StringContent(
                                          this.Data,
                                          Encoding.UTF8,
                                          "application/json")
                              };

            if (this.Headers != null)
            {
                foreach (var header in this.Headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            var response = cts == null
                               ? await this.client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                               : await this.client.SendAsync(
                                     request,
                                     HttpCompletionOption.ResponseHeadersRead,
                                     cts.Token);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}