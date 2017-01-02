namespace WinUX.Maths
{
    using System;

    /// <summary>
    /// Defines a collection of helper methods for Math expressions.
    /// </summary>
    public static class MathHelper
    {
        /// <summary>
        /// Checks whether two double values are close in value.
        /// </summary>
        /// <param name="value1">
        /// The first value.
        /// </param>
        /// <param name="value2">
        /// The second value.
        /// </param>
        /// <returns>
        /// Returns true if the values are close; else false.
        /// </returns>
        public static bool AreClose(double value1, double value2)
        {
            if (Math.Abs(value1 - value2) < 0.00005)
            {
                return true;
            }

            var a = (Math.Abs(value1) + Math.Abs(value2) + 10.0) * MathConstants.Epsilon;
            var b = value1 - value2;
            return (-a < b) && (a > b);
        }

        /// <summary>
        /// Checks whether a value is significantly greater than another.
        /// </summary>
        /// <param name="value1">
        /// The first value.
        /// </param>
        /// <param name="value2">
        /// The second value.
        /// </param>
        /// <returns>
        /// Returns true if the first value is greater than the second value; else false.
        /// </returns>
        public static bool IsGreaterThan(double value1, double value2)
        {
            return (value1 > value2) && !AreClose(value1, value2);
        }

        /// <summary>
        /// Checks whether a value is significantly less than another.
        /// </summary>
        /// <param name="value1">
        /// The first value.
        /// </param>
        /// <param name="value2">
        /// The second value.
        /// </param>
        /// <returns>
        /// Returns true if the first value is less than the second value; else false.
        /// </returns>
        public static bool IsLessThan(double value1, double value2)
        {
            return (value1 < value2) && !AreClose(value1, value2);
        }
    }
}