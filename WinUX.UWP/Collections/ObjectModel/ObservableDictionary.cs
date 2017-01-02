namespace WinUX.UWP.Collections.ObjectModel
{
    using System.Collections.Generic;
    using System.Linq;

    using Windows.Foundation.Collections;

    /// <summary>
    /// Defines a type of dictionary that supports raising a notification of changes from itself.
    /// </summary>
    public class ObservableDictionary : IObservableMap<string, object>
    {
        private readonly Dictionary<string, object> items = new Dictionary<string, object>();

        /// <inheritdoc />
        public event MapChangedEventHandler<string, object> MapChanged;

        /// <inheritdoc />
        public void Add(string key, object value)
        {
            this.items.Add(key, value);
            this.InvokeMapChanged(CollectionChange.ItemInserted, key);
        }

        /// <summary>
        /// Adds the specified <see cref="KeyValuePair{TKey,TValue}"/> to the dictionary.
        /// </summary>
        /// <param name="item">
        /// The <see cref="KeyValuePair{TKey,TValue}"/> to add.
        /// </param>
        public void Add(KeyValuePair<string, object> item)
        {
            this.Add(item.Key, item.Value);
        }

        /// <inheritdoc />
        public bool Remove(string key)
        {
            if (this.items.Remove(key))
            {
                this.InvokeMapChanged(CollectionChange.ItemRemoved, key);
                return true;
            }
            return false;
        }

        /// <inheritdoc />
        public bool Remove(KeyValuePair<string, object> item)
        {
            object currentValue;
            if (this.items.TryGetValue(item.Key, out currentValue) && object.Equals(item.Value, currentValue)
                && this.items.Remove(item.Key))
            {
                this.InvokeMapChanged(CollectionChange.ItemRemoved, item.Key);
                return true;
            }
            return false;
        }

        /// <inheritdoc />
        public object this[string key]
        {
            get
            {
                return this.items[key];
            }
            set
            {
                this.items[key] = value;
                this.InvokeMapChanged(CollectionChange.ItemChanged, key);
            }
        }

        /// <inheritdoc />
        public void Clear()
        {
            var priorKeys = this.items.Keys.ToArray();
            this.items.Clear();
            foreach (var key in priorKeys)
            {
                this.InvokeMapChanged(CollectionChange.ItemRemoved, key);
            }
        }

        /// <inheritdoc />
        public ICollection<string> Keys => this.items.Keys;

        /// <inheritdoc />
        public bool ContainsKey(string key)
        {
            return this.items.ContainsKey(key);
        }

        /// <inheritdoc />
        public bool TryGetValue(string key, out object value)
        {
            return this.items.TryGetValue(key, out value);
        }

        /// <inheritdoc />
        public ICollection<object> Values => this.items.Values;

        /// <inheritdoc />
        public bool Contains(KeyValuePair<string, object> item)
        {
            return this.items.Contains(item);
        }

        /// <inheritdoc />
        public int Count => this.items.Count;

        /// <inheritdoc />
        public bool IsReadOnly => false;

        /// <inheritdoc />
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        /// <inheritdoc />
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        /// <inheritdoc />
        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            int arraySize = array.Length;
            foreach (var pair in this.items)
            {
                if (arrayIndex >= arraySize) break;
                array[arrayIndex++] = pair;
            }
        }

        private void InvokeMapChanged(CollectionChange change, string key)
        {
            this.MapChanged?.Invoke(this, new ObservableDictionaryChangedEventArgs(change, key));
        }
    }
}