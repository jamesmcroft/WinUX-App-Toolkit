namespace WinUX.Xaml.VisualStateTriggers.NetworkConnectionTrigger
{
    using Windows.UI.Xaml;

    /// <summary>
    /// Defines the properties for the <see cref="NetworkConnectionTrigger"/>.
    /// </summary>
    public sealed partial class NetworkConnectionTrigger
    {
        /// <summary>
        /// Defines the dependency property for <see cref="ConnectionType"/>.
        /// </summary>
        public static readonly DependencyProperty ConnectionTypeProperty =
            DependencyProperty.Register(
                nameof(ConnectionType),
                typeof(NetworkConnectionType),
                typeof(NetworkConnectionTrigger),
                new PropertyMetadata(
                    NetworkConnectionType.Unknown,
                    (d, e) =>
                            ((NetworkConnectionTrigger)d).OnNetworkConnectionChanged((NetworkConnectionType)e.NewValue)));

        /// <summary>
        /// Gets or sets the expected connection type.
        /// </summary>
        public NetworkConnectionType ConnectionType
        {
            get
            {
                return (NetworkConnectionType)this.GetValue(ConnectionTypeProperty);
            }
            set
            {
                this.SetValue(ConnectionTypeProperty, value);
            }
        }
    }
}