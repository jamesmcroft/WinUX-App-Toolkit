namespace WinUX.Xaml.VisualStateTriggers.NetworkConnectionTrigger
{
    using System;

    using Windows.Networking.Connectivity;
    using Windows.UI.Core;

    using WinUX.Common;

    /// <summary>
    /// Defines a visual state trigger that checks a network connection type is in a specified state.
    /// </summary>
    public sealed partial class NetworkConnectionTrigger : VisualStateTriggerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkConnectionTrigger"/> class.
        /// </summary>
        public NetworkConnectionTrigger()
        {
            var eventListener = new WeakEventListener<NetworkConnectionTrigger, object>(this)
                                    {
                                        OnEventAction = (trigger, o) => this.OnNetworkStatusChanged(),
                                        OnDetachAction =
                                            (trigger, listener) =>
                                                    NetworkInformation.NetworkStatusChanged -= listener.OnEvent
                                    };

            NetworkInformation.NetworkStatusChanged += eventListener.OnEvent;
            this.OnNetworkConnectionChanged(this.ConnectionType);
        }

        private async void OnNetworkStatusChanged()
        {
            await this.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                    {
                        this.OnNetworkConnectionChanged(this.ConnectionType);
                    });
        }

        private void OnNetworkConnectionChanged(NetworkConnectionType newConnection)
        {
            var connectionProfile = NetworkInformation.GetInternetConnectionProfile();

            if (connectionProfile == null)
            {
                this.IsActive = newConnection == NetworkConnectionType.Disconnected;
            }
            else
            {
                var connectionState = connectionProfile.GetNetworkConnectivityLevel();

                if (connectionState != NetworkConnectivityLevel.InternetAccess)
                {
                    this.IsActive = newConnection == NetworkConnectionType.Disconnected;
                }
                else
                {
                    if (connectionProfile.NetworkAdapter.IanaInterfaceType.Equals(6))
                    {
                        this.IsActive = newConnection == NetworkConnectionType.Ethernet;
                    }
                    else if (connectionProfile.IsWlanConnectionProfile)
                    {
                        this.IsActive = newConnection == NetworkConnectionType.WiFi;
                    }
                    else if (connectionProfile.IsWwanConnectionProfile)
                    {
                        this.IsActive = newConnection == NetworkConnectionType.Mobile;
                    }
                }
            }
        }
    }
}