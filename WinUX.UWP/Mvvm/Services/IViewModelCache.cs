namespace WinUX.UWP.Mvvm.Services
{
    using System;

    /// <summary>
    /// Defines an interface for a view-model cache.
    /// </summary>
    public interface IViewModelCache
    {
        /// <summary>
        /// Adds a view-model object to the cache for the given <paramref name="identifier"/>.
        /// </summary>
        /// <param name="identifier">
        /// The navigation arguments corresponding to the view-model.
        /// </param>
        /// <param name="viewModel">
        /// The view-model.
        /// </param>
        void Add(Guid identifier, object viewModel);

        /// <summary>
        /// Gets a view-model from the cache for the given identifier.
        /// </summary>
        /// <typeparam name="TViewModel">
        /// The type of view model to retrieve.
        /// </typeparam>
        /// <param name="identifier">
        /// The navigation arguments to find a view model for.
        /// </param>
        /// <returns>
        /// Returns the stored view-model as the specified type.
        /// </returns>
        TViewModel Get<TViewModel>(Guid identifier) where TViewModel : class;

        /// <summary>
        /// Removes an existing view-model from the cache, if it exists.
        /// </summary>
        /// <param name="identifier">
        /// The identifier of the view-model to remove.
        /// </param>
        void Remove(Guid identifier);
    }
}