namespace WinUX
{
    using System;

    using WinUX.Maths;

    /// <summary>
    /// Defines a collection of extensions for handling extended math functions.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Performs a basic calculation of linear interpolation between two values.
        /// </summary>
        /// <param name="start">
        /// The start value.
        /// </param>
        /// <param name="end">
        /// The end value.
        /// </param>
        /// <param name="amount">
        /// The amount.
        /// </param>
        /// <returns>
        /// Returns the calculated result.
        /// </returns>
        public static float Lerp(this float start, float end, float amount)
        {
            var difference = end - start;
            var adjusted = difference * amount;
            return start + adjusted;
        }

        /// <summary>
        /// Checks whether a double value is zero.
        /// </summary>
        /// <param name="value">
        /// The value to check.
        /// </param>
        /// <returns>
        /// Returns true if zero; else false.
        /// </returns>
        public static bool IsZero(this double value)
        {
            return Math.Abs(value) < MathConstants.Epsilon;
        }

        /// <summary>
        /// Checks whether a float value is zero.
        /// </summary>
        /// <param name="value">
        /// The value to check.
        /// </param>
        /// <returns>
        /// Returns true if zero; else false.
        /// </returns>
        public static bool IsZero(this float value)
        {
            return Math.Abs(value) < MathConstants.Epsilon;
        }

        /// <summary>
        /// Checks whether a double value is not a number.
        /// </summary>
        /// <param name="value">
        /// The value to check.
        /// </param>
        /// <returns>
        /// Returns true if not a number; else false.
        /// </returns>
        public static bool IsNaN(this double value)
        {
            // Get the double as an unsigned long
            var union = new NanUnion { FloatingValue = value };

            // An IEEE 754 double precision floating point number is NaN if its
            // exponent equals 2047 and it has a non-zero mantissa.
            var exponent = union.IntegerValue & 0xfff0000000000000L;
            if ((exponent != 0x7ff0000000000000L) && (exponent != 0xfff0000000000000L))
            {
                return false;
            }
            var mantissa = union.IntegerValue & 0x000fffffffffffffL;
            return mantissa != 0L;
        }

        /// <summary>
        /// Checks whether the specified double value is close to the other in value.
        /// </summary>
        /// <param name="value1">
        /// The value to check against.
        /// </param>
        /// <param name="value2">
        /// The value to check with.
        /// </param>
        /// <returns>
        /// Returns true if the values are close; else false.
        /// </returns>
        public static bool IsCloseTo(this double value1, double value2)
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
        public static bool IsGreaterThan(this double value1, double value2)
        {
            return (value1 > value2) && !value1.IsCloseTo(value2);
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
        public static bool IsLessThan(this double value1, double value2)
        {
            return (value1 < value2) && !value1.IsCloseTo(value2);
        }
    }
}