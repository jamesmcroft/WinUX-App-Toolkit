namespace WinUX.Collections.Generic
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines methods to support the comparison of generic objects for equality.
    /// </summary>
    /// <typeparam name="T">
    /// The type of element to compare.
    /// </typeparam>
    public class GenericEqualityComparer<T> : IEqualityComparer<T>
        where T : class
    {
        private Func<T, object> Comparer { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericEqualityComparer{T}"/> class.
        /// </summary>
        /// <param name="comparer">
        /// The expression for comparing.
        /// </param>
        public GenericEqualityComparer(Func<T, object> comparer)
        {
            this.Comparer = comparer;
        }

        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <returns>
        /// Returns true if the specified objects are equal; otherwise, false.
        /// </returns>
        /// <param name="x">
        /// The first object of type <typeparamref name="T"/> to compare.
        /// </param>
        /// <param name="y">
        /// The second object of type <typeparamref name="T"/> to compare.
        /// </param>
        public bool Equals(T x, T y)
        {
            var first = this.Comparer.Invoke(x);
            var second = this.Comparer.Invoke(y);

            return first != null && first.Equals(second);
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <returns>
        /// A hash code for the specified object.
        /// </returns>
        /// <param name="obj">
        /// The <see cref="T:System.Object" /> for which a hash code is to be returned.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The type of <paramref name="obj" /> is a reference type and <paramref name="obj" /> is null.
        /// </exception>
        public int GetHashCode(T obj)
        {
            return this.Comparer.Invoke(obj).GetHashCode();
        }
    }
}