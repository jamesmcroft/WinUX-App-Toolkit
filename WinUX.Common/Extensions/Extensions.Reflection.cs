namespace WinUX
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Defines a collection of extensions for Reflection.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Gets a list of declared properties of the specified type.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// Returns a list of declared properties.
        /// </returns>
        public static IEnumerable<string> GetPropertiesAsList(this Type type)
        {
            var items = type.GetTypeInfo().DeclaredProperties.Select(p => p.Name).ToList();
            return items;
        }
    }
}