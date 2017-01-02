namespace WinUX.UWP.Application.ViewManagement
{
    using System;

    /// <summary>
    /// Handler for when a service has changed.
    /// </summary>
    /// <typeparam name="TService">
    /// The type of service.
    /// </typeparam>
    /// <param name="sender">
    /// The originator.
    /// </param>
    /// <param name="args">
    /// The service changed arguments containing the changed service and associated key.
    /// </param>
    public delegate void ViewServiceChangedEventHandler<TService>(
        object sender,
        ViewServiceChangedEventArgs<TService> args);

    /// <summary>
    /// Defines event arguments for a changed view service.
    /// </summary>
    /// <typeparam name="TService">
    /// The type of service.
    /// </typeparam>
    public sealed class ViewServiceChangedEventArgs<TService> : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewServiceChangedEventArgs{TService}"/> class.
        /// </summary>
        /// <param name="key">
        /// The key to the changed service.
        /// </param>
        /// <param name="service">
        /// The changed service.
        /// </param>
        public ViewServiceChangedEventArgs(int key, TService service)
        {
            this.Key = key;
            this.Service = service;
        }

        /// <summary>
        /// Gets the key relating to the changed service.
        /// </summary>
        public int Key { get; }

        /// <summary>
        /// Gets the service that has changed.
        /// </summary>
        public TService Service { get; }
    }
}