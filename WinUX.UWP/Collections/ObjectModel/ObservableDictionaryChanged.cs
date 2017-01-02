namespace WinUX.UWP.Collections.ObjectModel
{
    using Windows.Foundation.Collections;

    /// <summary>
    /// Defines the event arguments for when the <see cref="ObservableDictionary"/> collection changes.
    /// </summary>
    public class ObservableDictionaryChangedEventArgs : IMapChangedEventArgs<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableDictionaryChangedEventArgs"/> class.
        /// </summary>
        /// <param name="change">
        /// The type of collection change.
        /// </param>
        /// <param name="key">
        /// The key that has changes.
        /// </param>
        public ObservableDictionaryChangedEventArgs(CollectionChange change, string key)
        {
            this.CollectionChange = change;
            this.Key = key;
        }

        /// <summary>
        /// Gets the mode indicating which change occured to the collection.
        /// </summary>
        public CollectionChange CollectionChange { get; }

        /// <summary>
        /// Gets the key that changed.
        /// </summary>
        public string Key { get; }
    }
}