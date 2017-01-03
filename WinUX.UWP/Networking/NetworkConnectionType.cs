namespace WinUX.Networking
{
    /// <summary>
    /// Defines the enumeration values for a network's current connection type.
    /// </summary>
    public enum NetworkConnectionType
    {
        /// <summary>
        /// The current network type is unknown.
        /// </summary>
        Unknown,

        /// <summary>
        /// The current network type is disconnected.
        /// </summary>
        Disconnected,

        /// <summary>
        /// The current network type is over mobile.
        /// </summary>
        Mobile,

        /// <summary>
        /// The current network type is over Wi-Fi.
        /// </summary>
        WiFi,

        /// <summary>
        /// The current network type is over ethernet.
        /// </summary>
        Ethernet
    }
}