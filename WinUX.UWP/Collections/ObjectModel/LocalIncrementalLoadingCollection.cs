namespace WinUX.Collections.ObjectModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Runtime.InteropServices.WindowsRuntime;
    using System.Threading.Tasks;

    using Windows.Foundation;
    using Windows.UI.Xaml.Data;

    using WinUX.Extensions;

    /// <summary>
    /// Defines an <see cref="ObservableCollection{T}"/> that supports incremental loading of a collection of known items.
    /// </summary>
    /// <typeparam name="T">
    /// The type of elements contained within the collection.
    /// </typeparam>
    public sealed class LocalIncrementalLoadingCollection<T> : ObservableCollection<T>, ISupportIncrementalLoading
    {
        private List<T> allItems;

        private bool isLoadingItems;

        private int currentItemIdx;

        private int currentItemCount;

        /// <inheritdoc />
        public bool HasMoreItems { get; private set; }

        /// <summary>
        /// Initializes the <see cref="LocalIncrementalLoadingCollection{T}"/> with the items to load and the count of items to take when scrolling.
        /// </summary>
        /// <param name="items">
        /// The <see cref="List{T}"/> of items to load into the collection.
        /// </param>
        /// <param name="increment">
        /// The number of items to take when loading the next increment.
        /// </param>
        public void Initialize(List<T> items, int increment)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            this.HasMoreItems = false;

            this.Clear();

            this.allItems = items;

            this.currentItemIdx = 0;
            this.currentItemCount = increment;

            this.UpdateItemsCollection();
        }

        /// <summary>
        /// Initializes incremental loading from the view.
        /// </summary>
        /// <returns>
        /// The wrapped results of the load operation.
        /// </returns>
        /// <param name="count">
        /// The number of items to load.
        /// </param>
        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            if (this.isLoadingItems)
            {
                throw new InvalidOperationException(
                    "The LocalIncrementalLoadingCollection can only handle one operation in flight at a time.");
            }

            this.isLoadingItems = true;

            return AsyncInfo.Run(
                async ct =>
                    {
                        if (this.HasMoreItems)
                        {
                            this.UpdateItemsCollection();
                        }

                        await Task.Delay(1, ct);

                        this.isLoadingItems = false;

                        return this.HasMoreItems
                                   ? new LoadMoreItemsResult { Count = (uint)this.currentItemCount }
                                   : new LoadMoreItemsResult { Count = 0 };
                    });
        }

        private void UpdateItemsCollection()
        {
            this.AddRange(this.allItems.Take(this.currentItemIdx, this.currentItemCount));
            this.currentItemIdx = this.currentItemIdx + this.currentItemCount;
            this.HasMoreItems = this.Items.Count != this.allItems.Count;
        }
    }
}