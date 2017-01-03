namespace WinUX.Xaml.VisualStateTriggers.DeviceTrigger
{
    using Windows.Foundation.Metadata;
    using Windows.Graphics.Display;
    using Windows.UI.Xaml;

    /// <summary>
    /// Defines the properties for the <see cref="DeviceTrigger"/>.
    /// </summary>
    public sealed partial class DeviceTrigger
    {
        /// <summary>
        /// Defines the dependency property for <see cref="DeviceType"/>.
        /// </summary>
        public static readonly DependencyProperty DeviceTypeProperty = DependencyProperty.Register(
            nameof(DeviceType),
            typeof(DeviceType),
            typeof(DeviceTrigger),
            new PropertyMetadata(DeviceType.Unknown, OnDeviceTypeChanged));

        /// <summary>
        /// Defines the dependency property for <see cref="SupportsContinuum"/>.
        /// </summary>
        public static readonly DependencyProperty SupportsContinuumProperty =
            DependencyProperty.Register(
                nameof(SupportsContinuum),
                typeof(bool),
                typeof(DeviceTrigger),
                new PropertyMetadata(false));

        /// <summary>
        /// Gets or sets the expected device type.
        /// </summary>
        public DeviceType DeviceType
        {
            get
            {
                return (DeviceType)this.GetValue(DeviceTypeProperty);
            }
            set
            {
                this.SetValue(DeviceTypeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to support continuum for Mobile.
        /// </summary>
        public bool SupportsContinuum
        {
            get
            {
                return (bool)this.GetValue(SupportsContinuumProperty);
            }
            set
            {
                this.SetValue(SupportsContinuumProperty, value);
            }
        }

        private static double ScreenDiagonal
        {
            get
            {
                var di = DisplayInformation.GetForCurrentView();

                return ApiInformation.IsPropertyPresent(
                           typeof(DisplayInformation).ToString(),
                           nameof(di.DiagonalSizeInInches)) && di.DiagonalSizeInInches != null
                           ? di.DiagonalSizeInInches.Value
                           : 7;
            }
        }
    }
}