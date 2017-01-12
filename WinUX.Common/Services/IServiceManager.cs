namespace WinUX.Services
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines an interface for a service manager.
    /// </summary>
    /// <typeparam name="TKey">
    /// The type of key to reference by.
    /// </typeparam>
    /// <typeparam name="TService">
    /// The type of service to register.
    /// </typeparam>
    public interface IServiceManager<TKey, TService>
    {
        /// <summary>
        /// The service changed event.
        /// </summary>
        event ServiceChangedEventHandler<TKey, TService> ServiceChanged;

        /// <summary>
        /// Gets the collection of registered services.
        /// </summary>
        IReadOnlyDictionary<TKey, TService> Services { get; }

        /// <summary>
        /// Registers or updates a service in the manager.
        /// </summary>
        /// <param name="key">
        /// The key to register with.
        /// </param>
        /// <param name="service">
        /// The service to register.
        /// </param>
        /// <returns>
        /// Returns the registered service.
        /// </returns>
        TService RegisterOrUpdate(TKey key, TService service);

        /// <summary>
        /// Gets a registered service from the manager if exists.
        /// </summary>
        /// <param name="key">
        /// The key for the service.
        /// </param>
        /// <returns>
        /// Returns the registered service if exists; else null.
        /// </returns>
        TService Get(TKey key);

        /// <summary>
        /// Unregisters a service from the manager.
        /// </summary>
        /// <param name="key">
        /// The key to unregister.
        /// </param>
        /// <returns>
        /// Returns true if unregistered; else false.
        /// </returns>
        bool Unregister(TKey key);
    }
}