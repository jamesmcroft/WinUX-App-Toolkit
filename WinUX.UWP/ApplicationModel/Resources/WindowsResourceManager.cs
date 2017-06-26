namespace WinUX.ApplicationModel.Resources
{
    using System;

    using Windows.ApplicationModel.Resources.Core;

    using ResourceManager = WinUX.ApplicationModel.Resourcing.ResourceManager;

    /// <summary>
    /// Defines a provider for managing the retrieval of application string resources for a Windows application.
    /// </summary>
    public class WindowsResourceManager : ResourceManager
    {
        /// <inheritdoc />
        public override string GetResource(Type resourceAssemblyType, string resourceName)
        {
            var resource = base.GetResource(resourceAssemblyType, resourceName);

            if (string.IsNullOrWhiteSpace(resource))
            {
                resource = GetWindowsResource(resourceAssemblyType.GetAssemblyName(), resourceName);
            }

            return resource;
        }

        private static string GetWindowsResource(string assemblyName, string resourceName)
        {
            try
            {
                var resource =
                    Windows.ApplicationModel.Resources.Core.ResourceManager.Current.MainResourceMap.GetValue(
                        $"{assemblyName}/Resources/{resourceName}",
                        ResourceContext.GetForCurrentView());

                if (resource != null)
                {
                    return resource.ValueAsString;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            System.Diagnostics.Debug.WriteLine(
                $"Could not find resource '{resourceName}' in assembly '{assemblyName}'.");

            return string.Empty;
        }
    }
}