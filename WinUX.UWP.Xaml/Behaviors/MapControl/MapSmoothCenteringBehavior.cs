namespace WinUX.Xaml.Behaviors.MapControl
{
    using System;

    using Windows.Devices.Geolocation;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls.Maps;

    using Microsoft.Xaml.Interactivity;

    /// <summary>
    /// Defines a behavior for smoothly transitioning to a point on the MapControl.
    /// </summary>
    public sealed class MapSmoothCenteringBehavior : Behavior
    {
        private MapControl MapControl => this.AssociatedObject as MapControl;

        /// <summary>
        /// Defines the dependency property for <see cref="Center"/>.
        /// </summary>
        public static readonly DependencyProperty CenterProperty = DependencyProperty.Register(
            nameof(Center),
            typeof(Geopoint),
            typeof(MapSmoothCenteringBehavior),
            new PropertyMetadata(null, (d, e) => ((MapSmoothCenteringBehavior)d).UpdateMapCenter()));

        /// <summary>
        /// Defines the dependency property for <see cref="ZoomLevel"/>.
        /// </summary>
        public static readonly DependencyProperty ZoomLevelProperty = DependencyProperty.Register(
            nameof(ZoomLevel),
            typeof(double),
            typeof(MapSmoothCenteringBehavior),
            new PropertyMetadata(10));

        /// <summary>
        /// Gets or sets the center point to transition to.
        /// </summary>
        public Geopoint Center
        {
            get
            {
                return (Geopoint)this.GetValue(CenterProperty);
            }
            set
            {
                this.SetValue(CenterProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the zoom level to transition to.
        /// </summary>
        public double ZoomLevel
        {
            get
            {
                return (double)this.GetValue(ZoomLevelProperty);
            }
            set
            {
                this.SetValue(ZoomLevelProperty, value);
            }
        }

        private async void UpdateMapCenter()
        {
            if (this.MapControl != null && this.Center != null)
            {
                await this.MapControl.TrySetViewAsync(this.Center, this.ZoomLevel, null, null, MapAnimationKind.Bow);
            }
        }
    }
}