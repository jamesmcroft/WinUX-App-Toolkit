namespace WinUX.Collections.ObjectModel
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Defines a representation of a method that handles the changing of an item property in an ObservableItemCollection.
    /// </summary>
    /// <param name="sender">
    /// The originating sender.
    /// </param>
    /// <param name="e">
    /// The <see cref="NotifyCollectionItemPropertyChangedEventArgs"/>.
    /// </param>
    public delegate void NotifyCollectionItemPropertyChangedEventHandler(
        object sender,
        NotifyCollectionItemPropertyChangedEventArgs e);

    /// <summary>
    /// Defines event arguments for when a property has changed on an item in an <see cref="ObservableItemCollection{T}"/>.
    /// </summary>
    public class NotifyCollectionItemPropertyChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyCollectionItemPropertyChangedEventArgs"/> class.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <param name="e">
        /// The property changed arguments.
        /// </param>
        public NotifyCollectionItemPropertyChangedEventArgs(object item, int index, PropertyChangedEventArgs e)
        {
            this.Item = item;
            this.ItemIndex = index;
            this.ItemPropertyChangedArgs = e;
        }

        /// <summary>
        /// Gets the item that was changed.
        /// </summary>
        public object Item { get; }

        /// <summary>
        /// Gets the index of the item that was changed.
        /// </summary>
        public int ItemIndex { get; }

        /// <summary>
        /// Gets the property changed event arguments of the item.
        /// </summary>
        public PropertyChangedEventArgs ItemPropertyChangedArgs { get; }
    }
}