namespace WinUX
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Defines a collection of extensions for a type of collection.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <param name="collection">
        /// The <see cref="ICollection{T}"/> to add the specified collection of items to.
        /// </param>
        /// <param name="itemsToAdd">
        /// The collection whose elements should be added to the end of the <see cref="ICollection{T}"/>.
        /// </param>
        /// <typeparam name="T">
        /// The type of elements in the collection.
        /// </typeparam>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> itemsToAdd)
        {
            if (collection == null || itemsToAdd == null)
            {
                return;
            }

            foreach (var item in itemsToAdd)
            {
                collection.Add(item);
            }
        }

        /// <summary>
        /// Sorts a collection using the specified comparer.
        /// </summary>
        /// <typeparam name="T">
        /// The type of item in the collection.
        /// </typeparam>
        /// <typeparam name="TKey">
        /// The key.
        /// </typeparam>
        /// <param name="collection">
        /// The collection to sort.
        /// </param>
        /// <param name="comparer">
        /// The comparer to use.
        /// </param>
        public static void Sort<T, TKey>(this ObservableCollection<T> collection, Func<T, TKey> comparer)
        {
            if (collection == null || collection.Count <= 1) return;

            var idx = 0;
            foreach (var oldIndex in collection.OrderBy(comparer).Select(collection.IndexOf))
            {
                if (oldIndex != idx) collection.Move(oldIndex, idx);
                idx++;
            }
        }

        /// <summary>
        /// Sorts a collection using the specified comparer descending.
        /// </summary>
        /// <typeparam name="T">
        /// The type of item in the collection.
        /// </typeparam>
        /// <typeparam name="TKey">
        /// The key.
        /// </typeparam>
        /// <param name="collection">
        /// The collection to sort.
        /// </param>
        /// <param name="comparer">
        /// The comparer to use.
        /// </param>
        public static void SortDescending<T, TKey>(this ObservableCollection<T> collection, Func<T, TKey> comparer)
        {
            if (collection == null || collection.Count <= 1) return;

            var idx = 0;
            foreach (var oldIndex in collection.OrderByDescending(comparer).Select(collection.IndexOf))
            {
                if (oldIndex != idx) collection.Move(oldIndex, idx);
                idx++;
            }
        }

        /// <summary>
        /// Takes a number of elements from the specified collection from the specified starting index.
        /// </summary>
        /// <param name="list">
        /// The <see cref="List{T}"/> to take items from.
        /// </param>
        /// <param name="startingIndex">
        /// The index to start at in the <see cref="List{T}"/>.
        /// </param>
        /// <param name="takeCount">
        /// The number of items to take from the starting index of the <see cref="List{T}"/>.
        /// </param>
        /// <typeparam name="T">
        /// The type of elements in the collection.
        /// </typeparam>
        /// <returns>
        /// Returns a collection of <see cref="T"/> items.
        /// </returns>
        public static IEnumerable<T> Take<T>(this List<T> list, int startingIndex, int takeCount)
        {
            var results = new List<T>();

            int itemsToTake = takeCount;

            if (list.Count - 1 - startingIndex > itemsToTake)
            {
                var items = list.GetRange(startingIndex, itemsToTake);
                results.AddRange(items);
            }
            else
            {
                itemsToTake = list.Count - startingIndex;
                if (itemsToTake > 0)
                {
                    var items = list.GetRange(startingIndex, itemsToTake);
                    results.AddRange(items);
                }
            }

            return results;
        }

        /// <summary>
        /// Removes a collection of items from the specified <see cref="ICollection{T}"/>.
        /// </summary>
        /// <param name="collection">
        /// The collection to remove items from.
        /// </param>
        /// <param name="itemsToRemove">
        /// The items to remove.
        /// </param>
        /// <typeparam name="T">
        /// The type of items in the collection.
        /// </typeparam>
        public static void RemoveItems<T>(this ICollection<T> collection, IEnumerable<T> itemsToRemove)
        {
            if (collection == null)
            {
                return;
            }

            if (itemsToRemove == null)
            {
                return;
            }

            foreach (var item in itemsToRemove)
            {
                if (collection.Contains(item))
                {
                    collection.Remove(item);
                }
            }
        }

        /// <summary>
        /// Performs the specified action on each item in the collection.
        /// </summary>
        /// <typeparam name="T">
        /// The type of item in the collection.
        /// </typeparam>
        /// <param name="collection">
        /// The collection to action on.
        /// </param>
        /// <param name="action">
        /// The action to perform.
        /// </param>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action?.Invoke(item);
            }
        }

        /// <summary>
        /// Checks whether a collection contains all the elements of another.
        /// </summary>
        /// <typeparam name="T">
        /// The type of item in the collection.
        /// </typeparam>
        /// <param name="collection1">
        /// The collection to compare.
        /// </param>
        /// <param name="collection2">
        /// The collection to compare with.
        /// </param>
        /// <param name="comparer">
        /// The comparer.
        /// </param>
        /// <returns>
        /// Returns true if contains all; else false.
        /// </returns>
        public static bool ContainsAll<T>(
            this IEnumerable<T> collection1,
            IEnumerable<T> collection2,
            IEqualityComparer<T> comparer)
        {
            var container = new Dictionary<T, int>(comparer);

            foreach (var item in collection1)
            {
                if (container.ContainsKey(item))
                {
                    container[item]++;
                }
                else
                {
                    container.Add(item, 1);
                }
            }

            foreach (var item in collection2)
            {
                if (container.ContainsKey(item))
                {
                    container[item]--;
                }
                else
                {
                    return false;
                }
            }

            return container.Values.All(c => c == 0);
        }

        /// <summary>
        /// Converts a collection of objects to a comma separated string representing the objects.
        /// </summary>
        /// <param name="collection">
        /// The collection to join.
        /// </param>
        /// <typeparam name="T">
        /// The type of object within the collection.
        /// </typeparam>
        /// <returns>
        /// Returns a string representation of the items within the collection separated by a comma.
        /// </returns>
        public static string ToCommaSeparatedString<T>(this IEnumerable<T> collection)
        {
            return string.Join(",", collection);
        }

        /// <summary>
        /// Checks whether a collection of items is null or empty.
        /// </summary>
        /// <param name="collection">
        /// The collection to check.
        /// </param>
        /// <typeparam name="T">
        /// The type of object within the collection.
        /// </typeparam>
        /// <returns>
        /// Returns a boolean value indicating whether the collection is null or empty.
        /// </returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || !collection.Any();
        }

        /// <summary>
        /// Compares a collection of objects of a given type with another collection of objects with the same given type to see if they are the same.
        /// </summary>
        /// <param name="collection">
        /// The initial collection of items.
        /// </param>
        /// <param name="collectionB">
        /// The collection to compare with.
        /// </param>
        /// <typeparam name="T">
        /// The type of objects in the collections.
        /// </typeparam>
        /// <returns>
        /// Returns true if contains all the items in the collection; else false.
        /// </returns>
        public static bool Matches<T>(this IEnumerable<T> collection, IEnumerable<T> collectionB)
        {
            if (collection == null && collectionB == null)
            {
                return true;
            }

            if (collection == null)
            {
                return false;
            }

            if (collectionB == null)
            {
                return false;
            }

            var list1 = collection as IList<T> ?? collection.ToList();
            var list2 = collectionB as IList<T> ?? collectionB.ToList();

            return list1.ToList().Count == list2.ToList().Count
                   && list1.OrderBy(t => t).SequenceEqual(list2.OrderBy(t => t));
        }

        /// <summary>
        /// Sorts the items within the collection by the given key selector.
        /// </summary>
        /// <param name="collection">
        /// The collection to sort.
        /// </param>
        /// <param name="keySelector">
        /// The key selector.
        /// </param>
        /// <typeparam name="T">
        /// The type of item within the collection.
        /// </typeparam>
        /// <typeparam name="TKey">
        /// The type to order by.
        /// </typeparam>
        public static void SortBy<T, TKey>(this ObservableCollection<T> collection, Func<T, TKey> keySelector)
        {
            if (collection == null || collection.Count <= 1) return;

            var newIndex = 0;
            foreach (var oldIndex in collection.OrderBy(keySelector).Select(collection.IndexOf))
            {
                if (oldIndex != newIndex) collection.Move(oldIndex, newIndex);
                newIndex++;
            }
        }
    }
}