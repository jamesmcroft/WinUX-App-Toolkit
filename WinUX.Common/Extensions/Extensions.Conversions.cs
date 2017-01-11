namespace WinUX
{
    using System;

    /// <summary>
    /// Defines a collection of extensions for converting values to other values.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Converts a miles <see cref="double"/> value to meters.
        /// </summary>
        /// <param name="miles">
        /// The miles to convert.
        /// </param>
        /// <returns>
        /// Returns a <see cref="double"/> value representing the meters.
        /// </returns>
        public static double ToMeters(this double miles)
        {
            return miles / 0.00062137;
        }

        /// <summary>
        /// Converts a miles <see cref="int"/> value to meters.
        /// </summary>
        /// <param name="miles">
        /// The miles to convert.
        /// </param>
        /// <returns>
        /// Returns a <see cref="double"/> value representing the meters.
        /// </returns>
        public static double ToMeters(this int miles)
        {
            return miles / 0.00062137;
        }

        /// <summary>
        /// Converts a meters <see cref="double"/> value to miles.
        /// </summary>
        /// <param name="meters">
        /// The meters to convert.
        /// </param>
        /// <returns>
        /// Returns an <see cref="int"/> value representing the miles.
        /// </returns>
        public static int ToMiles(this double meters)
        {
            return (int)(meters * 0.00062137);
        }

        /// <summary>
        /// Converts a degrees <see cref="double"/> value to radians.
        /// </summary>
        /// <param name="degrees">
        /// The degrees to convert.
        /// </param>
        /// <returns>
        /// Returns a <see cref="double"/> value representing the radians.
        /// </returns>
        public static double ToRadians(this double degrees)
        {
            return degrees * Math.PI / 180.0;
        }
    }
}