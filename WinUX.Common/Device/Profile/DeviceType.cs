namespace WinUX.Device.Profile
{
    using WinUX.Attributes;

    /// <summary>
    /// Defines the enumeration values for a system's device type.
    /// </summary>
    public enum DeviceType
    {
        /// <summary>
        /// An unknown device type.
        /// </summary>
        [Description("New/Unknown device")]
        Unknown,

        /// <summary>
        /// A desktop device, e.g. Tablet, laptop, AiO.
        /// </summary>
        [Description("Windows Desktop")]
        Desktop,

        /// <summary>
        /// A Windows Phone device.
        /// </summary>
        [Description("Windows Mobile")]
        Mobile,

        /// <summary>
        /// A mobile device in continumm mode.
        /// </summary>
        [Description("Continuum for Mobile")]
        MobileContinuum,

        /// <summary>
        /// The Microsoft Surface Hub.
        /// </summary>
        [Description("Microsoft Surface Hub")]
        SurfaceHub,

        /// <summary>
        /// An IoT device.
        /// </summary>
        [Description("Windows 10 IOT")]
        IoT,

        /// <summary>
        /// The Xbox One
        /// </summary>
        [Description("Microsoft Xbox One")]
        Xbox,

        /// <summary>
        /// A Windows Holographic device, e.g. Microsoft HoloLens.
        /// </summary>
        [Description("Windows Holographic")]
        Holographic,

        /// <summary>
        /// An Android device, e.g. Samsung Galaxy.
        /// </summary>
        [Description("Android")]
        Android,

        /// <summary>
        /// An iOS device, e.g. iPhone or iPad.
        /// </summary>
        [Description("iOS")]
        iOS
    }
}