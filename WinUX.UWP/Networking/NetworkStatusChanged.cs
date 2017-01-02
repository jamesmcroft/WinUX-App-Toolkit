namespace WinUX.UWP.Networking
{
    using System;

    /// <summary>
    /// Handler for when a network connection has changed.
    /// </summary>
    /// <param name="sender">
    /// The originator.
    /// </param>
    /// <param name="args">
    /// The network connection changed arguments containing the new values.
    /// </param>
    public delegate void NetworkStatusChangedEventHandler(object sender, NetworkStatusChangedEventArgs args);

    /// <summary>
    /// Defines the event arguments for when the network connection changes.
    /// </summary>
    public sealed class NetworkStatusChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkStatusChangedEventArgs"/> class.
        /// </summary>
        /// <param name="profileName">
        /// The network profile name.
        /// </param>
        /// <param name="connectionType">
        /// The connection type.
        /// </param>
        /// <param name="mobileSignalState">
        /// The mobile connection type.
        /// </param>
        /// <param name="signal">
        /// The signal strength.
        /// </param>
        public NetworkStatusChangedEventArgs(
            string profileName,
            NetworkConnectionType connectionType,
            MobileNetworkConnectionType mobileSignalState,
            byte? signal)
        {
            this.Status = new NetworkStatus(profileName, connectionType, mobileSignalState, signal);
        }

        /// <summary>
        /// Gets the network status
        /// </summary>
        public NetworkStatus Status { get; }
    }
}