namespace WinUX.Networking
{
    public class NetworkStatusManager : INetworkStatusManager
    {
        /// <inheritdoc />
        public event NetworkStatusChangedEventHandler NetworkStatusChanged;

        /// <inheritdoc />
        public NetworkStatus CurrentNetworkStatus
            =>
                new NetworkStatus(
                    this.CurrentNetworkProfileName,
                    this.CurrentConnectionType,
                    this.CurrentMobileNetworkConnectionType,
                    this.CurrentNetworkSignal);

        /// <inheritdoc />
        public string CurrentNetworkProfileName { get; private set; }

        /// <inheritdoc />
        public NetworkConnectionType CurrentConnectionType { get; private set; }

        /// <inheritdoc />
        public MobileNetworkConnectionType CurrentMobileNetworkConnectionType { get; private set; }

        /// <inheritdoc />
        public byte? CurrentNetworkSignal { get; private set; }

        /// <inheritdoc />
        public void Initialize()
        {
            // ToDo; access Android network status
        }
    }
}