namespace WinUX
{
    using Windows.System.Profile;

    using WinUX.Device.Profile;

    /// <summary>
    /// Defines a collection of extensions for code regarding the device.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Gets the current <see cref="DeviceType"/> the application is running on.
        /// </summary>
        /// <param name="info">
        /// The current <see cref="AnalyticsVersionInfo"/>.
        /// </param>
        /// <returns>
        /// Returns the <see cref="DeviceType"/>.
        /// </returns>
        public static DeviceType GetDeviceType(this AnalyticsVersionInfo info)
        {
            var deviceFamily = info.DeviceFamily;

            switch (deviceFamily)
            {
                case "Windows.Desktop":
                    return DeviceType.Desktop;
                case "Windows.Mobile":
                    return DeviceType.Mobile;
                case "Windows.Team":
                    return DeviceType.SurfaceHub;
                case "Windows.IoT":
                    return DeviceType.IoT;
                case "Windows.Xbox":
                    return DeviceType.Xbox;
                case "Windows.HoloLens":
                case "Windows.Holographic":
                    return DeviceType.Holographic;
                default:
                    return DeviceType.Unknown;
            }
        }
    }
}