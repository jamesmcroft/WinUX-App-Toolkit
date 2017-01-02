namespace WinUX.Collections.ObjectModel
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// Defines a collection for items inheriting from <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of item in the collection.
    /// </typeparam>
    public class ObservableItemCollection<T> : ObservableCollection<T>, IDisposable
        where T : INotifyPropertyChanged
    {
        private bool enableCollectionChanged = true;

        /// <summary>
        /// The collection changed event.
        /// </summary>
        public override event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        /// The item property changed event.
        /// </summary>
        public event NotifyCollectionItemPropertyChangedEventHandler ItemPropertyChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableItemCollection{T}"/> class.
        /// </summary>
        public ObservableItemCollection()
        {
            base.CollectionChanged += (s, e) =>
                {
                    if (this.enableCollectionChanged)
                    {
                        this.CollectionChanged?.Invoke(this, e);
                    }
                };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableItemCollection{T}"/> class.
        /// </summary>
        /// <param name="collection">
        /// The collection to initialize with.
        /// </param>
        public ObservableItemCollection(IEnumerable<T> collection)
            : base(collection)
        {
            base.CollectionChanged += (s, e) =>
                {
                    if (this.enableCollectionChanged)
                    {
                        this.CollectionChanged?.Invoke(this, e);
                    }
                };
        }

        /// <summary>
        /// Raises the collection changed event with the provided arguments.
        /// </summary>
        /// <param name="e">
        /// Arguments of the event being raised.
        /// </param>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            this.CheckDisposed();
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    this.RegisterItemPropertyChangedEvent(e.NewItems);
                    break;
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Replace:
                    this.UnregisterItemPropertyChangedEvent(e.OldItems);
                    if (e.NewItems != null)
                    {
                        this.RegisterItemPropertyChangedEvent(e.NewItems);
                    }
                    break;
                case NotifyCollectionChangedAction.Move:
                case NotifyCollectionChangedAction.Reset:
                    break;
            }
            base.OnCollectionChanged(e);
        }

        /// <summary>
        /// Adds a range of items to the collection.
        /// </summary>
        /// <param name="items">
        /// The items to add.
        /// </param>
        public void AddRange(IEnumerable<T> items)
        {
            this.CheckDisposed();
            this.enableCollectionChanged = false;

            foreach (var item in items)
            {
                this.Add(item);
            }

            this.enableCollectionChanged = true;
            this.CollectionChanged?.Invoke(
                this,
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, items));
        }

        /// <summary>
        /// Removes a range of items from the collection.
        /// </summary>
        /// <param name="items">
        /// The items to remove.
        /// </param>
        public void RemoveRange(IEnumerable<T> items)
        {
            this.CheckDisposed();
            this.enableCollectionChanged = false;

            foreach (var item in items)
            {
                this.Remove(item);
            }

            this.enableCollectionChanged = true;
            this.CollectionChanged?.Invoke(
                this,
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, items));
        }

        /// <summary>
        /// Removes all items from the collection.
        /// </summary>
        protected override void ClearItems()
        {
            this.UnregisterItemPropertyChangedEvent(this);
            base.ClearItems();
        }

        private bool disposed;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this.disposed)
            {
                return;
            }

            this.ClearItems();
            this.disposed = true;
        }

        /// <summary>
        /// Checks whether the collection has been disposed.
        /// </summary>
        public void CheckDisposed()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }
        }

        private void RegisterItemPropertyChangedEvent(IEnumerable items)
        {
            this.CheckDisposed();
            foreach (var item in items.Cast<INotifyPropertyChanged>().Where(item => item != null))
            {
                item.PropertyChanged += this.Item_OnPropertyChanged;
            }
        }

        private void UnregisterItemPropertyChangedEvent(IEnumerable items)
        {
            this.CheckDisposed();
            foreach (var item in items.Cast<INotifyPropertyChanged>().Where(item => item != null))
            {
                item.PropertyChanged -= this.Item_OnPropertyChanged;
            }
        }

        private void Item_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.CheckDisposed();
            this.ItemPropertyChanged?.Invoke(
                this,
                new NotifyCollectionItemPropertyChangedEventArgs(sender, this.IndexOf((T)sender), e));
        }
    }
}