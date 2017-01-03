namespace WinUX.Application.ViewManagement
{
    using System;
    using System.Collections.Generic;

    using Windows.UI.ViewManagement;

    using WinUX.Services;

    /// <summary>
    /// Defines a generic service manager for application views.
    /// </summary>
    /// <typeparam name="TService">
    /// The type of service to register.
    /// </typeparam>
    public class ViewServiceManager<TService> : IServiceManager<int, TService>
    {
        private readonly Dictionary<int, TService> services;

        private static ViewServiceManager<TService> current;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewServiceManager{TService}"/> class.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the messenger is null.
        /// </exception>
        public ViewServiceManager()
        {
            this.services = new Dictionary<int, TService>();
        }

        /// <summary>
        /// Gets a static instance of the <see cref="ViewServiceManager{TService}"/>.
        /// </summary>
        public static ViewServiceManager<TService> Current => current ?? (current = new ViewServiceManager<TService>());

        /// <inheritdoc />
        public event ServiceChangedEventHandler<int, TService> ServiceChanged;

        /// <inheritdoc />
        public IReadOnlyDictionary<int, TService> Services => this.services;

        /// <summary>
        /// Gets the <see cref="TService"/> instance for the current view.
        /// </summary>
        /// <returns>
        /// Returns the service if exists; else null.
        /// </returns>
        public static TService GetForView()
        {
            var view = ApplicationView.GetForCurrentView();
            var viewId = view?.Id ?? -1;
            return Current.Get(viewId);
        }

        /// <summary>
        /// Registers a service for the current view.
        /// </summary>
        /// <param name="service">
        /// The service to register.
        /// </param>
        /// <returns>
        /// Returns the registered service.
        /// </returns>
        public static TService RegisterOrUpdateForView(TService service)
        {
            var view = ApplicationView.GetForCurrentView();
            var viewId = view?.Id ?? -1;
            return Current.RegisterOrUpdate(viewId, service);
        }

        /// <inheritdoc />
        public TService RegisterOrUpdate(int key, TService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            var exists = this.services.ContainsKey(key);
            if (exists)
            {
                var existing = this.services[key];
                if (existing.Equals(service))
                {
                    return existing;
                }

                this.services.Remove(key);
                this.services.Add(key, service);

                this.ServiceChanged?.Invoke(this, new ServiceChangedEventArgs<int, TService>(key, service));
            }
            else
            {
                this.services.Add(key, service);
            }

            return service;
        }

        /// <inheritdoc />
        public TService Get(int key)
        {
            var exists = this.services.ContainsKey(key);
            return !exists ? default(TService) : this.services[key];
        }

        /// <inheritdoc />
        public bool Unregister(int key)
        {
            var exists = this.services.ContainsKey(key);
            return exists && this.services.Remove(key);
        }
    }
}