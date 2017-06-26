namespace WinUX.ApplicationModel.Resourcing
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Defines a provider for managing the retrieval of application string resources.
    /// </summary>
    public class ResourceManager : IResourceManager
    {
        /// <inheritdoc />
        public virtual string GetResource(Type resourceAssemblyType, string resourceName)
        {
            try
            {
                var assemblyName = resourceAssemblyType.GetAssemblyName();
                var assembly = resourceAssemblyType.GetTypeInfo().Assembly;
                var resourceType = assembly.GetType($"{assemblyName}.Strings.Resources");

                var resourceProperty = resourceType?.GetTypeInfo().GetDeclaredProperty(resourceName);
                var resourceValue = resourceProperty?.GetValue(this, null);

                if (resourceValue != null)
                {
                    return resourceValue.ToString();
                }

                System.Diagnostics.Debug.WriteLine($"Could not find resource '{resourceName}' in assembly '{assemblyName}'.");
                return string.Empty;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            return string.Empty;
        }
    }
}