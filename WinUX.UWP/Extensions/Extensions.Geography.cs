namespace WinUX
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Windows.Devices.Geolocation;
    using Windows.UI.Xaml.Controls.Maps;

    using WinUX.Application;
    using WinUX.Maths;

    /// <summary>
    /// Defines a collection of extensions for geography, i.e. Bing Maps, MapControl and geo-position.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Shows the specified position in the Bing Maps application with the specified zoom level.
        /// </summary>
        /// <param name="geoposition">
        /// The position to show.
        /// </param>
        /// <param name="zoom">
        /// The zoom level to default to.
        /// </param>
        /// <returns>
        /// Returns an await-able task.
        /// </returns>
        public static async Task LaunchMapsAsync(this BasicGeoposition geoposition, double zoom)
        {
            var uri = new Uri($"bingmaps:?cp={geoposition.Latitude}~{geoposition.Longitude}&lvl={zoom}");
            await AppLauncher.LaunchAsync(uri, AppPackageFamilyNames.BingMaps, false);
        }

        /// <summary>
        /// Launches the Bing Maps application with the specified position in driving/navigation mode.
        /// </summary>
        /// <param name="position">
        /// The position to drive to.
        /// </param>
        /// <param name="locationName">
        /// The name to give the location.
        /// </param>
        /// <returns>
        /// Returns an await-able task.
        /// </returns>
        public static async Task LaunchMapNavigationAsync(this BasicGeoposition position, string locationName)
        {
            var uri =
                new Uri(
                    $"ms-drive-to:?destination.latitude={position.Latitude}&destination.longitude={position.Longitude}&destination.name={locationName}");
            await AppLauncher.LaunchAsync(uri, AppPackageFamilyNames.BingMaps, false);
        }

        /// <summary>
        /// Gets a specific point from the specified point with respect to the specified distance and bearing.
        /// </summary>
        /// <param name="geopoint">
        /// The point to get a point from.
        /// </param>
        /// <param name="distance">
        /// The distance away from the specified point.
        /// </param>
        /// <param name="bearing">
        /// The bearing from the specified point.
        /// </param>
        /// <returns>
        /// Returns a calculated <see cref="BasicGeoposition"/> at the distance and bearing from the specified point.
        /// </returns>
        public static BasicGeoposition GetPointAtDistanceAndBearing(
            this Geopoint geopoint,
            double distance,
            double bearing)
        {
            var radianLat = geopoint.Position.Latitude * MathConstants.DegreeToRadian;
            var radianLong = geopoint.Position.Longitude * MathConstants.DegreeToRadian;
            var angularDistance = distance / MathConstants.EarthRadius;
            var radianBearing = bearing * MathConstants.DegreeToRadian;

            var lat =
                Math.Asin(
                    Math.Sin(radianLat) * Math.Cos(angularDistance)
                    + Math.Cos(radianLat) * Math.Sin(angularDistance) * Math.Cos(radianBearing));

            var dlon = Math.Atan2(
                Math.Sin(radianBearing) * Math.Sin(angularDistance) * Math.Cos(radianLat),
                Math.Cos(angularDistance) - Math.Sin(radianLat) * Math.Sin(lat));

            var lon = ((radianLong + dlon + Math.PI) % (Math.PI * 2)) - Math.PI;

            var result = new BasicGeoposition
                             {
                                 Latitude = lat * MathConstants.RadianToDegree,
                                 Longitude = lon * MathConstants.RadianToDegree
                             };

            return result;
        }

        /// <summary>
        /// Gets a circle of 180 points around the specified center with respect to the specified radius. 
        /// </summary>
        /// <param name="center">
        /// The center point to get a circle of points around.
        /// </param>
        /// <param name="radius">
        /// The radius for the circle.
        /// </param>
        /// <returns>
        /// Returns a collection of <see cref="BasicGeoposition"/> forming the circle.
        /// </returns>
        public static IEnumerable<BasicGeoposition> GetCirclePoints(this Geopoint center, double radius)
        {
            return GetCirclePoints(center, radius, 180);
        }

        /// <summary>
        /// Gets a circle of points around the specified center with respect to the specified radius.
        /// </summary>
        /// <param name="center">
        /// The center point to get a circle of points around.
        /// </param>
        /// <param name="radius">
        /// The radius for the circle.
        /// </param>
        /// <param name="numberOfPoints">
        /// The number of circle points to generate. Defaults to 180; requires at least 3.
        /// </param>
        /// <returns>
        /// Returns a collection of <see cref="BasicGeoposition"/> forming the circle.
        /// </returns>
        public static IEnumerable<BasicGeoposition> GetCirclePoints(
            this Geopoint center,
            double radius,
            int numberOfPoints)
        {
            if (numberOfPoints < 3)
            {
                throw new InvalidOperationException("Cannot generate a circle of points without a minimum of 3 points.");
            }

            var angle = 360.0 / numberOfPoints;
            var locations = new List<BasicGeoposition>();

            for (var i = 0; i <= numberOfPoints; i++)
            {
                locations.Add(center.GetPointAtDistanceAndBearing(radius, angle * i));
            }

            return locations;
        }

        private static int zIndex = 1000;

        /// <summary>
        /// Brings a map element to the front of the <see cref="MapControl"/>.
        /// </summary>
        /// <param name="mapElement">
        /// The <see cref="MapElement"/>.
        /// </param>
        public static void BringToFront(this MapElement mapElement)
        {
            mapElement.ZIndex = zIndex;
            zIndex++;
        }
    }
}