namespace WinUX.Services
{
    using System;

    /// <summary>
    /// Handler for when a service has changed.
    /// </summary>
    /// <typeparam name="TKey">
    /// The type of key.
    /// </typeparam>
    /// <typeparam name="TService">
    /// The type of service.
    /// </typeparam>
    /// <param name="sender">
    /// The originator.
    /// </param>
    /// <param name="args">
    /// The service changed arguments containing the changed service and associated key.
    /// </param>
    public delegate void ServiceChangedEventHandler<TKey, TService>(
        object sender,
        ServiceChangedEventArgs<TKey, TService> args);

    /// <summary>
    /// Defines event arguments for a changed service.
    /// </summary>
    /// <typeparam name="TKey">The type of key.</typeparam>
    /// <typeparam name="TService">The type of service.</typeparam>
    public sealed class ServiceChangedEventArgs<TKey, TService> : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceChangedEventArgs{TKey,TService}"/> class.
        /// </summary>
        /// <param name="key">
        /// The key to the changed service.
        /// </param>
        /// <param name="service">
        /// The changed service.
        /// </param>
        public ServiceChangedEventArgs(TKey key, TService service)
        {
            this.Key = key;
            this.Service = service;
        }

        /// <summary>
        /// Gets the key relating to the changed service.
        /// </summary>
        public TKey Key { get; }

        /// <summary>
        /// Gets the service that has changed.
        /// </summary>
        public TService Service { get; }
    }
}