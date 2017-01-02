namespace WinUX.Maths
{
    using System;

    /// <summary>
    /// Defines a collection of math constants.
    /// </summary>
    public class MathConstants
    {
        /// <summary>
        /// Gets the value for Epsilon.
        /// </summary>
        public const double Epsilon = 2.2204460492503131E-16;

        /// <summary>
        /// Gets the value for Pi.
        /// </summary>
        public const double Pi = 3.1415926535897932384626433832795;

        /// <summary>
        /// Gets the value for a degree in radians.
        /// </summary>
        public const double DegreeToRadian = Math.PI / 180.0;

        /// <summary>
        /// Gets the value for a radian in degrees.
        /// </summary>
        public const double RadianToDegree = 180.0 / Math.PI;

        /// <summary>
        /// Gets the value of earth's radius in meters.
        /// </summary>
        public const double EarthRadius = 6378137.0;
    }
}