namespace WinUX.Common
{
    using System;

    /// <summary>
    /// Defines a collection of helper methods for parsing.
    /// </summary>
    public static class ParseHelper
    {
        /// <summary>
        /// Safely parses an object to a boolean.
        /// </summary>
        /// <param name="boolean">
        /// The boolean object.
        /// </param>
        /// <returns>
        /// Returns object parameter as a boolean.
        /// </returns>
        public static bool SafeParseBool(object boolean)
        {
            var parsedValue = false;
            if (boolean != null)
            {
                bool.TryParse(boolean.ToString(), out parsedValue);
            }
            return parsedValue;
        }

        /// <summary>
        /// Safely parses an object to an integer.
        /// </summary>
        /// <param name="integer">
        /// The integer object.
        /// </param>
        /// <returns>
        /// Returns object parameter as an integer.
        /// </returns>
        public static int SafeParseInt(object integer)
        {
            var parsedValue = 0;
            if (integer != null)
            {
                int.TryParse(integer.ToString(), out parsedValue);
            }
            return parsedValue;
        }

        /// <summary>
        /// Safely parse an object to an Int64 object.
        /// </summary>
        /// <param name="integer">
        /// The integer.
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        public static long SafeParseInt64(object integer)
        {
            long parsedValue = 0;

            if (integer != null)
            {
                long.TryParse(integer.ToString(), out parsedValue);
            }

            return parsedValue;
        }

        /// <summary>
        /// Safely parse an object to a Guid object.
        /// </summary>
        /// <param name="guid">
        /// The guid.
        /// </param>
        /// <returns>
        /// The <see cref="Guid"/>.
        /// </returns>
        public static Guid SafeParseGuid(object guid)
        {
            var parsedValue = Guid.Empty;

            if (guid != null)
            {
                Guid.TryParse(guid.ToString(), out parsedValue);
            }

            return parsedValue;
        }

        /// <summary>
        /// Safely parses an object to a double.
        /// </summary>
        /// <param name="dbl">
        /// The double object.
        /// </param>
        /// <returns>
        /// Returns object parameter as a double.
        /// </returns>
        public static double SafeParseDouble(object dbl)
        {
            double parsedValue = 0;
            if (dbl != null)
            {
                double.TryParse(dbl.ToString(), out parsedValue);
            }
            return parsedValue;
        }

        /// <summary>
        /// Safely parses an object to a decimal.
        /// </summary>
        /// <param name="dcml">
        /// The decimal object.
        /// </param>
        /// <returns>
        /// Returns object parameter as a decimal.
        /// </returns>
        public static decimal SafeParseDecimal(object dcml)
        {
            decimal parsedValue = 0;
            if (dcml != null)
            {
                decimal.TryParse(dcml.ToString(), out parsedValue);
            }
            return parsedValue;
        }

        /// <summary>
        /// Safely parses an object to a string.
        /// </summary>
        /// <param name="str">
        /// The string object.
        /// </param>
        /// <returns>
        /// Returns object parameter as a string.
        /// </returns>
        public static string SafeParseString(object str)
        {
            string parsedValue = string.Empty;
            if (str != null)
            {
                parsedValue = str.ToString();
            }
            return parsedValue;
        }

        /// <summary>
        /// Safely parses an object to a float.
        /// </summary>
        /// <param name="flt">
        /// The float object.
        /// </param>
        /// <returns>
        /// The <see cref="float"/>.
        /// </returns>
        public static float SafeParseFloat(object flt)
        {
            float parsedValue = 0;

            if (flt != null)
            {
                float.TryParse(flt.ToString(), out parsedValue);
            }

            return parsedValue;
        }

        /// <summary>
        /// Safely parses an object to an enum value.
        /// </summary>
        /// <param name="enumValue">
        /// The enum object.
        /// </param>
        /// <typeparam name="TEnum">
        /// The type of Enum.
        /// </typeparam>
        /// <returns>
        /// Returns object parameter as an enum value.
        /// </returns>
        public static TEnum SafeParseEnum<TEnum>(object enumValue) where TEnum : struct
        {
            var parsedValue = default(TEnum);
            if (enumValue != null)
            {
                Enum.TryParse(enumValue.ToString(), out parsedValue);
            }
            return parsedValue;
        }

        /// <summary>
        /// Safely parses an object to a <see cref="DateTime"/> value.
        /// </summary>
        /// <param name="dateTime">
        /// The date time object.
        /// </param>
        /// <returns>
        /// Returns object parameter as a DateTime.
        /// </returns>
        public static DateTime SafeParseDateTime(object dateTime)
        {
            var parsedValue = DateTime.MinValue;
            if (dateTime != null)
            {
                DateTime.TryParse(dateTime.ToString(), out parsedValue);
            }
            return parsedValue;
        }

        /// <summary>
        /// Safely parses an object to a <see cref="DateTimeOffset"/> value.
        /// </summary>
        /// <param name="dateTimeOffset">
        /// The date time offset object.
        /// </param>
        /// <returns>
        /// Returns object parameter as a DateTimeOffset.
        /// </returns>
        public static DateTimeOffset SafeParseDateTimeOffset(object dateTimeOffset)
        {
            var parsedValue = DateTimeOffset.MinValue;
            if (dateTimeOffset != null)
            {
                DateTimeOffset.TryParse(dateTimeOffset.ToString(), out parsedValue);
            }
            return parsedValue;
        }
    }
}