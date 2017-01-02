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
    public sealed class NetworkRequestManager
    {
        private readonly ConcurrentDictionary<string, NetworkRequestCallback> currentQueue =
            new ConcurrentDictionary<string, NetworkRequestCallback>();

        private Timer processTimer;

        private bool isProcessingRequests;

        /// <summary>
        /// Starts the manager processing the queue of network requests at a default time period of 1 minute.
        /// </summary>
        public void Start()
        {
            this.Start(TimeSpan.FromMinutes(1));
        }

        /// <summary>
        /// Starts the manager processing the queue of network requests.
        /// </summary>
        /// <param name="processPeriod">
        /// The time period between each process of the queue.
        /// </param>
        public void Start(TimeSpan processPeriod)
        {
            if (this.processTimer == null)
            {
                this.processTimer = new Timer(
                    state => this.ProcessQueue(),
                    null,
                    TimeSpan.FromMinutes(0),
                    processPeriod);
            }
            else
            {
                this.processTimer.Change(TimeSpan.FromMinutes(0), processPeriod);
            }
        }

        /// <summary>
        /// Stops the processing of the network manager queues.
        /// </summary>
        public void Stop()
        {
            this.processTimer?.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
        }

        /// <summary>
        /// Called to process the current queue of network requests.
        /// </summary>
        public void ProcessQueue()
        {
            if (this.isProcessingRequests)
            {
                return;
            }

            if (this.currentQueue.Count > 0)
            {
                return;
            }

            this.isProcessingRequests = true;

            try
            {
                var cts = new CancellationTokenSource();
                var requestTasks = new List<Task>();
                var requestCallbacks = new List<NetworkRequestCallback>();

                while (this.currentQueue.Count > 0)
                {
                    NetworkRequestCallback request;
                    if (this.currentQueue.TryRemove(this.currentQueue.FirstOrDefault().Key, out request))
                    {
                        requestCallbacks.Add(request);
                    }
                }

                foreach (var container in requestCallbacks)
                {
                    requestTasks.Add(ExecuteRequestsAsync(this.currentQueue, container, cts));
                }
            }
            finally
            {
                this.isProcessingRequests = false;
            }
        }

        /// <summary>
        /// Adds or updates a network request in the queue.
        /// </summary>
        /// <typeparam name="TRequest">
        /// The type of network request.
        /// </typeparam>
        /// <typeparam name="TResponse">
        /// The expected response type.
        /// </typeparam>
        /// <param name="request">
        /// The network request to execute.
        /// </param>
        /// <param name="successCallback">
        /// The action to execute when receiving a successful response.
        /// </param>
        public void AddOrUpdate<TRequest, TResponse>(TRequest request, Action<TResponse> successCallback)
            where TRequest : NetworkRequest
        {
            this.AddOrUpdate<TRequest, TResponse, Exception>(request, successCallback, null);
        }

        /// <summary>
        /// Adds or updates a network request in the queue.
        /// </summary>
        /// <typeparam name="TRequest">
        /// The type of network request.
        /// </typeparam>
        /// <typeparam name="TResponse">
        /// The expected response type.
        /// </typeparam>
        /// <typeparam name="TErrorResponse">
        /// The expected error response type.
        /// </typeparam>
        /// <param name="request">
        /// The network request to execute.
        /// </param>
        /// <param name="successCallback">
        /// The action to execute when receiving a successful response.
        /// </param>
        /// <param name="errorCallback">
        /// The action to execute when receiving an error response.
        /// </param>
        public void AddOrUpdate<TRequest, TResponse, TErrorResponse>(
            TRequest request,
            Action<TResponse> successCallback,
            Action<TErrorResponse> errorCallback) where TRequest : NetworkRequest
        {
            var weakSuccessCallback = new WeakReferenceCallback(successCallback, typeof(TResponse));
            var weakErrorCallback = new WeakReferenceCallback(errorCallback, typeof(TErrorResponse));
            var requestCallback = new NetworkRequestCallback(request, weakSuccessCallback, weakErrorCallback);

            this.currentQueue.AddOrUpdate(
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