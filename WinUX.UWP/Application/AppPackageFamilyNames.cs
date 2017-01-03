namespace WinUX.Application
{
    using Windows.ApplicationModel;

    /// <summary>
    /// Defines a collection of application package family names for common applications.
    /// </summary>
    public static class AppPackageFamilyNames
    {
        /// <summary>
        /// Defines the application package family name for this application.
        /// </summary>
        public static string Current => Package.Current.Id.FamilyName;

        /// <summary>
        /// Defines the application package family name for the Maps application.
        /// </summary>
        public static readonly string BingMaps = "Microsoft.WindowsMaps_8wekyb3d8bbwe";
    }
}