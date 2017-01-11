namespace WinUX.Application
{
    using System;
    using System.Threading.Tasks;

    using Windows.System;

    /// <summary>
    /// Defines methods for handling the launch of applications.
    /// </summary>
    public static class AppLauncher
    {
        /// <summary>
        /// Launches an application with the specified application ID using the specified uri URI.
        /// </summary>
        /// <param name="uri">
        /// The URI to pass to the application.
        /// </param>
        /// <param name="applicationPackageFamilyName">
        /// The package family name of the application to launch.
        /// </param>
        /// <param name="promptToLaunch">
        /// A value indicating whether the prompt to launch.
        /// </param>
        /// <returns>
        /// Returns an awaitable task.
        /// </returns>
        public static async Task LaunchAsync(Uri uri, string applicationPackageFamilyName, bool promptToLaunch)
        {
            var options = new LauncherOptions
                              {
                                  TargetApplicationPackageFamilyName = applicationPackageFamilyName,
                                  TreatAsUntrusted = promptToLaunch
                              };

            await Launcher.LaunchUriAsync(uri, options);
        }
    }
}