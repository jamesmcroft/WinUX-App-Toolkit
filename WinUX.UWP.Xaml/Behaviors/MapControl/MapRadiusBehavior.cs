namespace WinUX.Xaml.Behaviors.MapControl
{
    using System.Linq;

    using Windows.Devices.Geolocation;
    using Windows.UI;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls.Maps;

    using Microsoft.Xaml.Interactivity;

    /// <summary>
    /// Defines a behavior for creating a radius around a center point on the MapControl.
    /// </summary>
    public sealed class MapRadiusBehavior : Behavior
    {
        /// <summary>
        /// Defines the dependency property for <see cref="RadiusCenter"/>.
        /// </summary>
        public static readonly DependencyProperty RadiusCenterProperty =
            DependencyProperty.Register(
                nameof(RadiusCenter),
                typeof(Geopoint),
                typeof(MapRadiusBehavior),
                new PropertyMetadata(null, (d, e) => ((MapRadiusBehavior)d).UpdateRadius()));

        /// <summary>
        /// Defines the dependency property for <see cref="Radius"/>.
        /// </summary>
        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(
            nameof(Radius),
            typeof(double),
            typeof(MapRadiusBehavior),
            new PropertyMetadata(0.0, (d, e) => ((MapRadiusBehavior)d).UpdateRadius()));

        /// <summary>
        /// Gets or sets the radius center point.
        /// </summary>
        public Geopoint RadiusCenter
        {
            get
            {
                return (Geopoint)this.GetValue(RadiusCenterProperty);
            }
            set
            {
                this.SetValue(RadiusCenterProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the radius.
        /// </summary>
        public double Radius
        {
            get
            {
                return (double)this.GetValue(RadiusProperty);
            }
            set
            {
                this.SetValue(RadiusProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the radius fill color.
        /// </summary>
        public Color RadiusFillColor { get; set; }

        /// <summary>
        /// Gets or sets the radius border color.
        /// </summary>
        public Color RadiusBorderColor { get; set; }

        private MapPolygon MapRadiusPolygon { get; set; }

        private MapControl MapControl => this.AssociatedObject as MapControl;

        private void UpdateRadius()
        {
            if (this.MapControl == null || this.RadiusCenter == null) return;

            var radiusCirclePoints = this.RadiusCenter.GetCirclePoints(this.Radius);

            if (this.RadiusCenter != null)
            {
                var currentMapRadiusPolygon =
                    this.MapControl.MapElements.FirstOrDefault(x => x.Equals(this.MapRadiusPolygon));

                if (currentMapRadiusPolygon != null)
                {
                    this.MapControl.MapElements.Remove(currentMapRadiusPolygon);
                }
            }

            this.MapRadiusPolygon = new MapPolygon
                                        {
                                            Path = new Geopath(radiusCirclePoints),
                                            ZIndex = 0,
                                            FillColor = this.RadiusFillColor,
                                            StrokeColor = this.RadiusBorderColor
                                        };

            this.MapControl.MapElements.Add(this.MapRadiusPolygon);
        }
    }
}