namespace WinUX.UWP.Mvvm.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines a provider for caching view-models.
    /// </summary>
    public class ViewModelCache : IViewModelCache
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

        /// <inheritdoc />
        public void Add(Guid identifier, object viewModel)
        {
            var cacheData = this.GetCacheData(identifier);
            if (cacheData.Value != null) return;

            this.Cache.Add(identifier, viewModel);
        }

        /// <inheritdoc />
        public TViewModel Get<TViewModel>(Guid identifier) where TViewModel : class
        {
            var cache = this.GetCacheData(identifier);
            return cache.Value as TViewModel;
        }

        /// <inheritdoc />
        public void Remove(Guid identifier)
        {
            if (identifier == Guid.Empty)
            {
                return;
            }

            var cacheData = this.GetCacheData(identifier);
            if (cacheData.Value != null)
            {
                this.Cache.Remove(cacheData.Key);
            }
        }

        private KeyValuePair<Guid, object> GetCacheData(Guid identifier)
        {
            return this.Cache.FirstOrDefault(x => x.Key == identifier);
        }
    }
}