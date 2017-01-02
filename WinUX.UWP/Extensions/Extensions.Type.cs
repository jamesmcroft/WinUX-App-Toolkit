namespace WinUX.UWP.Extensions
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Windows.UI.Xaml;

    using WinUX.Common.Date;

    /// <summary>
    /// Defines a collection of extensions for Types.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Gets the <see cref="Type"/> from the specified typeName.
        /// </summary>
        /// <param name="typeName">
        /// The type name as a <see cref="string"/>.
        /// </param>
        /// <param name="searchLocal">
        /// Indicates whether to search in WinUX namespace.
        /// </param>
        /// <param name="searchWindows">
        /// Indicates whether to search in Windows namespace.
        /// </param>
        /// <returns>
        /// Returns the <see cref="Type"/> if exists; else null.
        /// </returns>
        public static Type GetTypeByName(this string typeName, bool searchLocal, bool searchWindows)
        {
            var result = Type.GetType(typeName);
            if (result != null)
            {
                return result;
            }

            // Search in System
            var proxyType = DateTimeKind.Local;
            var assembly = proxyType.GetType().GetTypeInfo().Assembly;

            foreach (var typeInfo in assembly.ExportedTypes)
            {
                if (typeInfo.Name == typeName)
                {
                    return typeInfo;
                }
            }

            if (searchWindows)
            {
                // Search in Windows
                var windowsProxyType = VerticalAlignment.Center;
                assembly = windowsProxyType.GetType().GetTypeInfo().Assembly;

                foreach (var typeInfo in assembly.ExportedTypes)
                {
                    if (typeInfo.Name == typeName)
                    {
                        return typeInfo;
                    }
                }
            }

            if (searchLocal)
            {
                // Search in project
                var projectProxyType = StateOfDay.Morning;
                assembly = projectProxyType.GetType().GetTypeInfo().Assembly;

                return assembly.ExportedTypes.FirstOrDefault(typeInfo => typeInfo.Name == typeName);
            }

            return null;
        }
    }
}
