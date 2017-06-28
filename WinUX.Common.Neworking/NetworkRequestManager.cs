namespace WinUX.Networking
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using WinUX.Common;
    using WinUX.Networking.Requests;

    /// <summary>
    /// Defines a manager for executing queued network requests.
    /// </summary>
    public sealed class NetworkRequestManager : INetworkRequestManager
    {
        private Timer processTimer;

        private bool isProcessingRequests;

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkRequestManager"/> class.
        /// </summary>
        public NetworkRequestManager()
        {
            this.CurrentQueue = new ConcurrentDictionary<string, NetworkRequestCallback>();
        }

        /// <inheritdoc />
        public ConcurrentDictionary<string, NetworkRequestCallback> CurrentQueue { get; }

        /// <inheritdoc />
        public void Start()
        {
            this.Start(TimeSpan.FromMinutes(1));
        }

        /// <inheritdoc />
        public void Start(TimeSpan processPeriod)
        {
            if (this.processTimer == null)
            {
                this.processTimer = new Timer(
                    state => this.ProcessCurrentQueue(),
                    null,
                    TimeSpan.FromMinutes(0),
                    processPeriod);
            }
            else
            {
                this.processTimer.Change(TimeSpan.FromMinutes(0), processPeriod);
            }
        }

        /// <inheritdoc />
        public void Stop()
        {
            this.processTimer?.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
        }

        /// <inheritdoc />
        public void ProcessCurrentQueue()
        {
            if (this.isProcessingRequests)
            {
                return;
            }

            if (this.CurrentQueue.Count > 0)
            {
                return;
            }

            this.isProcessingRequests = true;

            try
            {
                var cts = new CancellationTokenSource();
                var requestTasks = new List<Task>();
                var requestCallbacks = new List<NetworkRequestCallback>();

                while (this.CurrentQueue.Count > 0)
                {
                    NetworkRequestCallback request;
                    if (this.CurrentQueue.TryRemove(this.CurrentQueue.FirstOrDefault().Key, out request))
                    {
                        requestCallbacks.Add(request);
                    }
                }

                foreach (var container in requestCallbacks)
                {
                    requestTasks.Add(ExecuteRequestsAsync(this.CurrentQueue, container, cts));
                }
            }
            finally
            {
                this.isProcessingRequests = false;
            }
        }

        /// <inheritdoc />
        public void AddOrUpdate<TRequest, TResponse>(TRequest request, Action<TResponse> successCallback)
            where TRequest : NetworkRequest
        {
            this.AddOrUpdate<TRequest, TResponse, Exception>(request, successCallback, null);
        }

        /// <inheritdoc />
        public void AddOrUpdate<TRequest, TResponse, TErrorResponse>(
            TRequest request,
            Action<TResponse> successCallback,
            Action<TErrorResponse> errorCallback) where TRequest : NetworkRequest
        {
            var weakSuccessCallback = new WeakReferenceCallback(successCallback, typeof(TResponse));
            var weakErrorCallback = new WeakReferenceCallback(errorCallback, typeof(TErrorResponse));
            var requestCallback = new NetworkRequestCallback(request, weakSuccessCallback, weakErrorCallback);

            this.CurrentQueue.AddOrUpdate(
                request.Identifier.ToString(),
                requestCallback,
                (s, callback) => requestCallback);
        }

        private static async Task ExecuteRequestsAsync(
            ConcurrentDictionary<string, NetworkRequestCallback> queue,
            NetworkRequestCallback requestCallback,
            CancellationTokenSource cts)
        {
            if (cts.IsCancellationRequested)
            {
                queue.AddOrUpdate(
                    requestCallback.Request.Identifier.ToString(),
                    requestCallback,
                    (s, callback) => requestCallback);

                return;
            }

            var request = requestCallback.Request;
            var successCallback = requestCallback.SuccessCallback;
            var errorCallback = requestCallback.ErrorCallback;

            try
            {
                var response = await request.ExecuteAsync(successCallback.Type);
                successCallback.Invoke(response);
            }
            catch (Exception ex)
            {
                successCallback.Invoke(Activator.CreateInstance(successCallback.Type));
                errorCallback.Invoke(ex);
            }
        }
    }
}