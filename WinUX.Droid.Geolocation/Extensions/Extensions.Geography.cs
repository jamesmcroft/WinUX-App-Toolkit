namespace WinUX.Geolocation.Extensions
{
    using System;
    using System.Collections.Generic;

    using WinUX.Maths;

    using XPlat.Device.Geolocation;

    /// <summary>
    /// Defines a collection of extensions for geography, i.e. maps and geo-position.
    /// </summary>
    public static class Extensions
    {
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
        /// Returns a calculated <see cref="Geocoordinate"/> at the distance and bearing from the specified point.
        /// </returns>
        public static Geocoordinate GetPointAtDistanceAndBearing(
            this Geocoordinate geopoint,
            double distance,
            double bearing)
        {
            var radianLat = geopoint.Latitude * MathConstants.DegreeToRadian;
            var radianLong = geopoint.Longitude * MathConstants.DegreeToRadian;
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

            var result = new Geocoordinate
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
        /// Returns a collection of <see cref="Geocoordinate"/> forming the circle.
        /// </returns>
        public static IEnumerable<Geocoordinate> GetCirclePoints(this Geocoordinate center, double radius)
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
        /// Returns a collection of <see cref="Geocoordinate"/> forming the circle.
        /// </returns>
        public static IEnumerable<Geocoordinate> GetCirclePoints(
            this Geocoordinate center,
            double radius,
            int numberOfPoints)
        {
            if (numberOfPoints < 3)
            {
                throw new InvalidOperationException("Cannot generate a circle of points without a minimum of 3 points.");
            }

            var angle = 360.0 / numberOfPoints;
            var locations = new List<Geocoordinate>();

            for (var i = 0; i <= numberOfPoints; i++)
            {
                locations.Add(center.GetPointAtDistanceAndBearing(radius, angle * i));
            }

            return locations;
        }
    }
}