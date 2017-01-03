namespace WinUX.Networking
{
    /// <summary>
    /// Defines a model for representing a network status.
    /// </summary>
    public sealed class NetworkStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkStatus"/> class.
        /// </summary>
        /// <param name="profileName">
        /// The network profile name.
        /// </param>
        /// <param name="connectionType">
        /// The connection type.
        /// </param>
        /// <param name="mobileNetworkConnectionType">
        /// The mobile connection type.
        /// </param>
        /// <param name="signal">
        /// The signal strength.
        /// </param>
        public NetworkStatus(
            string profileName,
            NetworkConnectionType connectionType,
            MobileNetworkConnectionType mobileNetworkConnectionType,
            byte? signal)
        {
            this.ProfileName = profileName;
            this.ConnectionType = connectionType;
            this.MobileNetworkConnectionType = mobileNetworkConnectionType;
            this.Signal = signal;
        }

        /// <summary>
        /// Gets the current connection profile name.
        /// </summary>
        public string ProfileName { get; }

        /// <summary>
        /// Gets the current connection type.
        /// </summary>
        public NetworkConnectionType ConnectionType { get; }

        /// <summary>
        /// Gets the current mobile connection type.
        /// </summary>
        public MobileNetworkConnectionType MobileNetworkConnectionType { get; }

        /// <summary>
        /// Gets the current mobile signal strength.
        /// </summary>
        public byte? Signal { get; }
    }
}