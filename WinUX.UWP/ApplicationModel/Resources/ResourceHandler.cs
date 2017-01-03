namespace WinUX.ApplicationModel.Resources
{
    using System;

    using Windows.ApplicationModel.Resources.Core;

    using WinUX.Diagnostics.Tracing;

    /// <summary>
    /// Defines helper methods for handling application resources in code.
    /// </summary>
    public static class ResourceHandler
    {
        /// <summary>
        /// Gets a string resource from the assembly calling this method within the specified resource area by the specified resource name.
        /// </summary>
        /// <param name="resourceArea">
        /// The file area containing the strings in the containing assembly, e.g. Resources, Dialogs, Errors.
        /// </param>
        /// <param name="resourceName">
        /// The name of the resource to retrieve in the contained resource file.
        /// </param>
        /// <returns>
        /// Returns the resource string if it exists; else string.Empty.
        /// </returns>
        public static string GetResource(string resourceArea, string resourceName)
        {
            try
            {
                var resource = ResourceManager.Current.MainResourceMap.GetValue(
                    $"{resourceArea}/{resourceName}",
                    ResourceContext.GetForCurrentView());

                if (resource != null)
                {
                    return resource.ValueAsString;
                }
            }
            catch (Exception ex)
            {
                EventLogger.Current.WriteDebug(ex.Message);
            }

            EventLogger.Current.WriteDebug(
                $"Could not find resource '{resourceArea}/{resourceName}' in the current view resources.");

            return string.Empty;
        }
    }
}