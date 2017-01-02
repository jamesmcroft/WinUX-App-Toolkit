namespace WinUX
{
    using System;
    using System.Globalization;

    using WinUX.Common.Date;

    /// <summary>
    /// Defines a collection of extensions for date and time.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Gets the <see cref="DateTime"/> value representing the start day of the week of the specified <see cref="DateTime"/>.
        /// </summary>
        /// <param name="dateTime">
        /// The <see cref="DateTime"/> to get the start day of the week value from.
        /// </param>
        /// <returns>
        /// Returns the start day of the week <see cref="DateTime"/>.
        /// </returns>
        public static DateTime GetStartOfWeek(this DateTime dateTime)
        {
            var info = CultureInfo.CurrentCulture.DateTimeFormat;
            var firstDay = info.FirstDayOfWeek;
            var diff = dateTime.DayOfWeek - firstDay;
            if (diff < 0)
            {
                diff += 7;
            }

            return dateTime.AddDays(-diff);
        }

        /// <summary>
        /// Gets the suffix for the current day of the specified <see cref="DateTime"/>.
        /// </summary>
        /// <param name="dateTime">
        /// The <see cref="DateTime"/> to get the current day suffix value from.
        /// </param>
        /// <returns>
        /// Returns the suffix of the specified day as a <see cref="string"/>.
        /// </returns>
        public static string GetDaySuffix(this DateTime dateTime)
        {
            switch (dateTime.Day)
            {
                case 1:
                case 21:
                case 31:
                    return "st";
                case 2:
                case 22:
                    return "nd";
                case 3:
                case 23:
                    return "rd";
                default:
                    return "th";
            }
        }

        /// <summary>
        /// Checks a <see cref="DateTime"/> against the current date to determine the age.
        /// </summary>
        /// <param name="dateTime">
        /// The <see cref="DateTime"/> to compare.
        /// </param>
        /// <returns>
        /// Returns the age as an <see cref="int"/>.
        /// </returns>
        public static int ToAge(this DateTime dateTime)
        {
            var age = DateTime.Now.Year - dateTime.Year;
            if (DateTime.Now < dateTime.AddYears(age))
            {
                age--;
            }

            return age;
        }

        /// <summary>
        /// Gets the current state of day of the specified <see cref="DateTime"/>.
        /// </summary>
        /// <param name="dateTime">
        /// The <see cref="DateTime"/> to get the state of day.
        /// </param>
        /// <returns>
        /// Returns Morning if between 6AM and 12PM, Afternoon if between 12PM and 6PM, else Evening.
        /// </returns>
        public static StateOfDay ToStateOfDay(this DateTime dateTime)
        {
            var hours = dateTime.Hour;

            return hours >= 6 && hours < 12
                       ? StateOfDay.Morning
                       : (hours >= 12 && hours < 18 ? StateOfDay.Afternoon : StateOfDay.Evening);
        }

        /// <summary>
        /// Compares the <see cref="DateTime"/> to see if it is less than the specified <see cref="DateTime"/>.
        /// </summary>
        /// <param name="dateTime">
        /// The <see cref="DateTime"/>.
        /// </param>
        /// <param name="minDate">
        /// The <see cref="DateTime"/> to compare less than.
        /// </param>
        /// <returns>
        /// Returns true if the specified <see cref="DateTime"/> is less than the <see cref="DateTime"/>; else false.
        /// </returns>
        public static bool LessThan(this DateTime dateTime, DateTime minDate)
        {
            DateTime dateTime1 = dateTime.AddSeconds(-1 * dateTime.Second);
            DateTime dateTime2 = minDate.AddSeconds(-1 * minDate.Second);

            if (dateTime1.Date < dateTime2.Date)
            {
                return true;
            }

            if (dateTime1.Date == dateTime2.Date)
            {
                var timeSpan1 = new TimeSpan(dateTime1.Hour, dateTime1.Minute, 0);
                var timeSpan2 = new TimeSpan(dateTime2.Hour, dateTime2.Minute, 0);

                if (timeSpan1 < timeSpan2)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Compares the <see cref="DateTime"/> to see if it is greater than the specified <see cref="DateTime"/>.
        /// </summary>
        /// <param name="dateTime">
        /// The <see cref="DateTime"/>.
        /// </param>
        /// <param name="maxDate">
        /// The <see cref="DateTime"/> to compare greater than.
        /// </param>
        /// <returns>
        /// Returns true if the specified <see cref="DateTime"/> is greater than the <see cref="DateTime"/>; else false.
        /// </returns>
        public static bool GreaterThan(this DateTime dateTime, DateTime maxDate)
        {
            DateTime dateTime1 = dateTime.AddSeconds(-1 * dateTime.Second);
            DateTime dateTime2 = maxDate.AddSeconds(-1 * maxDate.Second);

            if (dateTime1.Date > dateTime2.Date)
            {
                return true;
            }

            if (dateTime1.Date == dateTime2.Date)
            {
                var timeSpan1 = new TimeSpan(dateTime1.Hour, dateTime1.Minute, 0);
                var timeSpan2 = new TimeSpan(dateTime2.Hour, dateTime2.Minute, 0);

                if (timeSpan1 > timeSpan2)
                {
                    return true;
                }
            }

            return false;
        }

    }
}