namespace WinUX
{
    using System;

    /// <summary>
    /// Defines a collection of extensions for geography.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Calculates the distance between a latitude/longitude of one location with another.
        /// </summary>
        /// <param name="latitudeA">
        /// The latitude for location A.
        /// </param>
        /// <param name="longitudeA">
        /// The longitude for location A.
        /// </param>
        /// <param name="latitudeB">
        /// The latitude for location B.
        /// </param>
        /// <param name="longitudeB">
        /// The longitude for location B.
        /// </param>
        /// <returns>
        /// Returns a <see cref="double"/> value representing the distance between.
        /// </returns>
        public static double CalculateDistanceBetween(
            double latitudeA,
            double longitudeA,
            double latitudeB,
            double longitudeB)
        {
            double circumference = 40000.0; // Earth's circumference at the equator in km
            double distance;

            double latRadiansA = latitudeA.ToRadians();
            double lonRadiansA = longitudeA.ToRadians();
            double latRadiansB = latitudeB.ToRadians();
            double lonRadiansB = longitudeB.ToRadians();

            double longitudeDiff = Math.Abs(lonRadiansA - lonRadiansB);

            if (longitudeDiff > Math.PI)
            {
                longitudeDiff = (2.0 * Math.PI) - longitudeDiff;
            }

            double angleCalculation =
                Math.Acos(
                    (Math.Sin(latRadiansB) * Math.Sin(latRadiansA))
                    + ((Math.Cos(latRadiansB) * Math.Cos(latRadiansA)) * Math.Cos(longitudeDiff)));

            distance = circumference * angleCalculation / (2.0 * Math.PI);

            return distance;
        }

        /// <summary>
        /// Incidates whether a given latitude/longitude is within a radius of another.
        /// </summary>
        /// <param name="latitude">
        /// The latitude to check.
        /// </param>
        /// <param name="longitude">
        /// The longitude to check.
        /// </param>
        /// <param name="centreLatitude">
        /// The centre latitude.
        /// </param>
        /// <param name="centreLongitude">
        /// The centre longitude.
        /// </param>
        /// <param name="radiusMeters">
        /// The radius meters.
        /// </param>
        /// <returns>
        /// Returns a <see cref="bool"/> value indicating whether the lat/lon is within the radius.
        /// </returns>
        public static bool IsPointWithinRadius(
            double latitude,
            double longitude,
            double centreLatitude,
            double centreLongitude,
            double radiusMeters)
        {
            var distanceMetres = CalculateDistanceBetween(latitude, longitude, centreLatitude, centreLongitude) * 1000;
            return distanceMetres < radiusMeters;
        }
    }
}