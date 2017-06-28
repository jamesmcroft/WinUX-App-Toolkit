namespace WinUX.Networking
{
    using System;

    using Windows.Networking.Connectivity;

    /// <summary>
    /// Defines a helper for the current network status of the device running the application.
    /// </summary>
    public sealed class NetworkStatusManager : INetworkStatusManager
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
            NetworkInformation.NetworkStatusChanged += this.OnNetworkStatusChanged;
            this.OnNetworkStatusChanged(this);
        }

        private void OnNetworkStatusChanged(object sender)
        {
            var currentConnectionType = this.CurrentConnectionType;
            var currentMobileNetworkConnectionType = this.CurrentMobileNetworkConnectionType;
            var currentNetworkSignal = this.CurrentNetworkSignal;

            try
            {
                var currentConnectionProfile = NetworkInformation.GetInternetConnectionProfile();
                if (currentConnectionProfile == null)
                {
                    this.CurrentNetworkProfileName = "Unknown";
                    this.CurrentConnectionType = NetworkConnectionType.Unknown;
                    this.CurrentMobileNetworkConnectionType = MobileNetworkConnectionType.Unknown;
                    this.CurrentNetworkSignal = null;
                }
                else
                {
                    this.CurrentNetworkProfileName = currentConnectionProfile.ProfileName;
                    this.CurrentNetworkSignal = currentConnectionProfile.GetSignalBars();

                    if (currentConnectionProfile.NetworkAdapter.IanaInterfaceType.Equals(6))
                    {
                        this.CurrentConnectionType = NetworkConnectionType.Ethernet;
                        this.CurrentMobileNetworkConnectionType = MobileNetworkConnectionType.Unknown;
                    }
                    else if (currentConnectionProfile.IsWlanConnectionProfile)
                    {
                        this.CurrentConnectionType = NetworkConnectionType.WiFi;
                        this.CurrentMobileNetworkConnectionType = MobileNetworkConnectionType.Unknown;
                    }
                    else if (currentConnectionProfile.IsWwanConnectionProfile)
                    {
                        this.CurrentConnectionType = NetworkConnectionType.Mobile;

                        var networkClass = currentConnectionProfile.WwanConnectionProfileDetails.GetCurrentDataClass();

                        if (networkClass.HasFlag(WwanDataClass.LteAdvanced)
                            || networkClass.HasFlag(WwanDataClass.Cdma1xEvdo)
                            || networkClass.HasFlag(WwanDataClass.Cdma1xEvdoRevA)
                            || networkClass.HasFlag(WwanDataClass.Cdma1xEvdoRevB)
                            || networkClass.HasFlag(WwanDataClass.Cdma1xEvdv)
                            || networkClass.HasFlag(WwanDataClass.Cdma1xRtt)
                            || networkClass.HasFlag(WwanDataClass.Cdma3xRtt)
                            || networkClass.HasFlag(WwanDataClass.CdmaUmb))
                        {
                            this.CurrentMobileNetworkConnectionType = MobileNetworkConnectionType.FourG;
                        }
                        else if (networkClass.HasFlag(WwanDataClass.Hsdpa) || networkClass.HasFlag(WwanDataClass.Hsupa)
                                 || networkClass.HasFlag(WwanDataClass.Umts))
                        {
                            this.CurrentMobileNetworkConnectionType = MobileNetworkConnectionType.ThreeG;
                        }
                        else if (networkClass.HasFlag(WwanDataClass.Edge) || networkClass.HasFlag(WwanDataClass.Gprs))
                        {
                            this.CurrentMobileNetworkConnectionType = MobileNetworkConnectionType.TwoG;
                        }
                        else
                        {
                            this.CurrentMobileNetworkConnectionType = MobileNetworkConnectionType.Unknown;
                        }
                    }
                    else
                    {
                        this.CurrentConnectionType = NetworkConnectionType.Unknown;
                        this.CurrentMobileNetworkConnectionType = MobileNetworkConnectionType.Unknown;
                    }
                }

                if (this.CurrentConnectionType != currentConnectionType
                    || this.CurrentMobileNetworkConnectionType != currentMobileNetworkConnectionType
                    || this.CurrentNetworkSignal != currentNetworkSignal)
                {
                    this.NetworkStatusChanged?.Invoke(
                        this,
                        new NetworkStatusChangedEventArgs(
                            this.CurrentNetworkProfileName,
                            this.CurrentConnectionType,
                            this.CurrentMobileNetworkConnectionType,
                            this.CurrentNetworkSignal));
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.ToString());
#endif
            }
        }
    }
}