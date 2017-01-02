namespace WinUX
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines a collection of extensions for dictionaries.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Gets a value from a dictionary based on the specified key.
        /// </summary>
        /// <param name="dictionary">
        /// The <see cref="Dictionary{TKey,TValue}"/>.
        /// </param>
        /// <param name="key">
        /// The key to locate the value for.
        /// </param>
        /// <typeparam name="TKey">
        /// The type of key in the dictionary.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// The type of value in the dictionary.
        /// </typeparam>
        /// <returns>
        /// Returns the value if it exists; otherwise default.
        /// </returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
        {
            var result = default(TValue);

            if (dictionary != null && dictionary.ContainsKey(key))
            {
                result = dictionary[key];
            }

            return result;
        }
    }
}