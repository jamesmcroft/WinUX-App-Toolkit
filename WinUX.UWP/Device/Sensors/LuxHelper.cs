namespace WinUX.Device.Sensors
{
    using System;

    using Windows.Devices.Sensors;
    using Windows.UI.Xaml;

    using WinUX.Common.Date;

    /// <summary>
    /// Defines helper methods for handling Light sensors.
    /// </summary>
    public sealed class LuxHelper
    {
        private static LuxHelper current;

        /// <summary>
        /// Gets an instance of the <see cref="LuxHelper"/>.
        /// </summary>
        public static LuxHelper Current => current ?? (current = new LuxHelper());

        private LightSensor sensor;

        /// <summary>
        /// Initializes a new instance of the <see cref="LuxHelper"/> class.
        /// </summary>
        public LuxHelper()
        {
            this.sensor = LightSensor.GetDefault();
        }

        /// <summary>
        /// Gets the current application theme based on the light level.
        /// </summary>
        public ApplicationTheme Theme
        {
            get
            {
                if (this.sensor == null)
                {
                    this.sensor = LightSensor.GetDefault();
                }

                if (this.sensor != null)
                {
                    var reading = this.sensor.GetCurrentReading();
                    return reading.IlluminanceInLux > 5 ? ApplicationTheme.Light : ApplicationTheme.Dark;
                }

                var state = DateTime.UtcNow.ToStateOfDay();
                return state == StateOfDay.Morning || state == StateOfDay.Afternoon
                           ? ApplicationTheme.Light
                           : ApplicationTheme.Dark;
            }
        }
    }
}