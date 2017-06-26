namespace WinUX.ApplicationModel.Resourcing
{
    using System;

    /// <summary>
    /// Defines an interface for an application string resource manager.
    /// </summary>
    public interface IResourceManager
    {
        /// <summary>
        /// Gets a string resource from the specified assembly type by the specified resource name.
        /// </summary>
        /// <param name="resourceAssemblyType">
        /// The assembly type containing the resources to retrieve from.
        /// </param>
        /// <param name="resourceName">
        /// The name of the resource to retrieve in the contained resource file.
        /// </param>
        /// <remarks>
        /// String resources file must be in the Strings folder at the root of the library with the file name 'Resources'.
        /// </remarks>
        /// <returns>
        /// Returns the resource string if it exists; else string.Empty.
        /// </returns>
        string GetResource(Type resourceAssemblyType, string resourceName);
    }
}