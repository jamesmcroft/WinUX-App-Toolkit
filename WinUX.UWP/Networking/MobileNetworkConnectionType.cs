namespace WinUX.UWP.Networking
{
    using WinUX.Attributes;

    /// <summary>
    /// Defines the enumeration values for a network's current mobile connection type.
    /// </summary>
    public enum MobileNetworkConnectionType
    {
        /// <summary>
        /// The current mobile network type is unknown.
        /// </summary>
        Unknown,

        /// <summary>
        /// The current mobile network type is 2G.
        /// </summary>
        [Description("2G")]
        TwoG,

        /// <summary>
        /// The current mobile network type is 3G.
        /// </summary>
        [Description("3G")]
        ThreeG,

        /// <summary>
        /// The current mobile network type is 4G/LTE.
        /// </summary>
        [Description("4G")]
        FourG
    }
}