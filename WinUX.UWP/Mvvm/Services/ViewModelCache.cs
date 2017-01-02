namespace WinUX.UWP.Mvvm.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines a provider for caching view-models.
    /// </summary>
    public sealed class ViewModelCache
    {
        private static ViewModelCache current;

        /// <summary>
        /// Gets an instance of the <see cref="ViewModelCache"/>.
        /// </summary>
        public static ViewModelCache Current => current ?? (current = new ViewModelCache());

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelCache"/> class.
        /// </summary>
        public ViewModelCache()
        {
            this.Cache = new Dictionary<Guid, object>();
        }

        private Dictionary<Guid, object> Cache { get; }

        /// <summary>
        /// Adds a <see cref="object"/> containing the view model for the given <paramref name="identifier"/>.
        /// </summary>
        /// <param name="identifier">
        /// The navigation arguments corresponding to the view model.
        /// </param>
        /// <param name="viewModel">
        /// The view model.
        /// </param>
        public void AddViewModel(Guid identifier, object viewModel)
        {
            var cacheData = this.GetCacheData(identifier);
            if (cacheData.Value != null) return;

            this.Cache.Add(identifier, viewModel);
        }

        /// <summary>
        /// Gets a <see cref="object"/> containing the view model for the given navigation args.
        /// </summary>
        /// <typeparam name="T">
        /// The type of view model to retrieve.
        /// </typeparam>
        /// <param name="identifer">
        /// The navigation arguments to find a view model for.
        /// </param>
        /// <returns>
        /// Returns an <see cref="object"/> as a view model.
        /// </returns>
        public T GetViewModel<T>(Guid identifer) where T : class
        {
            var cache = this.GetCacheData(identifer);
            return cache.Value as T;
        }

        /// <summary>
        /// Removes an existing view model from the cache.
        /// </summary>
        /// <param name="identifier">
        /// The identifier of the view-model to remove.
        /// </param>
        public void RemoveIfExists(Guid identifier)
        {
            if (identifier != Guid.Empty)
            {
                var cacheData = this.GetCacheData(identifier);
                if (cacheData.Value != null)
                {
                    this.Cache.Remove(cacheData.Key);
                }
            }
        }

        private KeyValuePair<Guid, object> GetCacheData(Guid identifier)
        {
            return this.Cache.FirstOrDefault(x => x.Key == identifier);
        }
    }
}