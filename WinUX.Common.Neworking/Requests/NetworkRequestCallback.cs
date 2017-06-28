namespace WinUX.Networking.Requests
{
    using System;

    using WinUX.Common;

    /// <summary>
    /// Defines a model for a network request callback.
    /// </summary>
    public sealed class NetworkRequestCallback
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkRequestCallback"/> class.
        /// </summary>
        /// <param name="request">
        /// The network request.
        /// </param>
        public NetworkRequestCallback(NetworkRequest request)
            : this(request, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkRequestCallback"/> class.
        /// </summary>
        /// <param name="request">
        /// The network request.
        /// </param>
        /// <param name="successCallback">
        /// The success callback.
        /// </param>
        public NetworkRequestCallback(NetworkRequest request, WeakReferenceCallback successCallback)
            : this(request, successCallback, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkRequestCallback"/> class.
        /// </summary>
        /// <param name="request">
        /// The network request.
        /// </param>
        /// <param name="successCallback">
        /// The success callback.
        /// </param>
        /// <param name="errorCallback">
        /// The error callback.
        /// </param>
        public NetworkRequestCallback(
            NetworkRequest request,
            WeakReferenceCallback successCallback,
            WeakReferenceCallback errorCallback)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            this.Request = request;
            this.SuccessCallback = successCallback;
            this.ErrorCallback = errorCallback;
        }

        /// <summary>
        /// Gets the network process.
        /// </summary>
        public NetworkRequest Request { get; }

        /// <summary>
        /// Gets the success callback.
        /// </summary>
        public WeakReferenceCallback SuccessCallback { get; }

        /// <summary>
        /// Gets the error callback.
        /// </summary>
        public WeakReferenceCallback ErrorCallback { get; }
    }
}