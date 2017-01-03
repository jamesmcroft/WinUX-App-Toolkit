namespace WinUX.Networking
{
    /// <summary>
    /// Defines an interface for a network status manager.
    /// </summary>
    public interface INetworkStatusManager
    {
        /// <summary>
        /// Occurs when the network status changes.
        /// </summary>
        event NetworkStatusChangedEventHandler NetworkStatusChanged;

        /// <summary>
        /// Gets the current network status.
        /// </summary>
        NetworkStatus CurrentNetworkStatus { get; }

        /// <summary>
        /// Gets the current network profile name.
        /// </summary>
        string CurrentNetworkProfileName { get; }

        /// <summary>
        /// Gets the current network connection type.
        /// </summary>
        NetworkConnectionType CurrentConnectionType { get; }

        /// <summary>
        /// Gets the current mobile network connection type.
        /// </summary>
        MobileNetworkConnectionType CurrentMobileNetworkConnectionType { get; }

        /// <summary>
        /// Gets the current network signal value.
        /// </summary>
        byte? CurrentNetworkSignal { get; }

        /// <summary>
        /// Initializes the network status manager.
        /// </summary>
        void Initialize();
    }
}
