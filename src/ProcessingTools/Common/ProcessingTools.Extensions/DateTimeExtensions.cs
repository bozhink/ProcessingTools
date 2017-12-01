// <copyright file="DateTimeExtensions.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions
{
    using System;
    using System.Globalization;
    using ProcessingTools.Constants;
    using ProcessingTools.Enumerations;

    /// <summary>
    /// <see cref="DateTime"/> extensions.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Gets Unix start date.
        /// </summary>
        public static DateTime UnixStartDate => new DateTime(year: 1970, month: 1, day: 1, hour: 0, minute: 0, second: 0, millisecond: 0);

        /// <summary>
        /// Parse string to formatted <see cref="DateTime" />.
        /// </summary>
        /// <param name="s">String to be parsed.</param>
        /// <param name="format">Format string.</param>
        /// <returns>Parsed date.</returns>
        public static DateTime? ParseFormat(string s, string format)
        {
            if (DateTime.TryParseExact(s: s, format: format, provider: CultureInfo.InvariantCulture, style: DateTimeStyles.AllowWhiteSpaces, result: out DateTime result))
            {
                return result == DateTime.MinValue ? (DateTime?)null : result;
            }

            return null;
        }

        /// <summary>
        /// Returns a Unix timestamp that represents the current moment.
        /// </summary>
        /// <returns>Now expressed as a Unix timestamp.</returns>
        /// <remarks>
        /// See https://stackoverflow.com/questions/17632584/how-to-get-the-unix-timestamp-in-c-sharp
        /// </remarks>
        public static int GetUnixTimestamp()
        {
            return (int)Math.Truncate(DateTime.UtcNow.Subtract(UnixStartDate).TotalSeconds);
        }

        /// <summary>
        /// Converts a given <see cref="DateTime"/> into a Unix timestamp.
        /// </summary>
        /// <param name="instance">This instance.</param>
        /// <returns>The given <see cref="DateTime"/> in Unix timestamp format.</returns>
        /// <remarks>
        /// See https://stackoverflow.com/questions/17632584/how-to-get-the-unix-timestamp-in-c-sharp
        /// </remarks>
        public static int ToUnixTimestamp(this DateTime instance)
        {
            return (int)Math.Truncate(instance.ToUniversalTime().Subtract(UnixStartDate).TotalSeconds);
        }

        /// <summary>
        /// Converts a given <see cref="int"/> as Unix timestamp into a <see cref="DateTime"/> object.
        /// </summary>
        /// <param name="timestamp"><see cref="int"/> as Unix timestamp.</param>
        /// <returns><see cref="DateTime"/> value of the given <see cref="int"/> as Unix timestamp.</returns>
        /// <remarks>
        /// See https://www.unixtimeconverter.io/1924988400
        /// </remarks>
        public static DateTime ToUnixTimestamp(this int timestamp)
        {
            return UnixStartDate.AddSeconds(timestamp);
        }

        /// <summary>
        /// Returns a new <see cref="DateTime"/> that represents the first day in the instance month.
        /// </summary>
        /// <param name="instance">This instance.</param>
        /// <returns>New <see cref="DateTime"/> that represents the first day in the instance month.</returns>
        public static DateTime First(this DateTime instance)
        {
            return instance.AddDays(1 - instance.Day);
        }

        /// <summary>
        /// Returns a new <see cref="DateTime"/> that represents the first specified day in the instance month.
        /// </summary>
        /// <param name="instance">This instance.</param>
        /// <param name="dayOfWeek">The resultant day of week.</param>
        /// <returns>New <see cref="DateTime"/> that represents the first specified day in the instance month.</returns>
        public static DateTime First(this DateTime instance, DayOfWeek dayOfWeek)
        {
            DateTime first = instance.First();

            if (first.DayOfWeek != dayOfWeek)
            {
                first = first.Next(dayOfWeek);
            }

            return first;
        }

        /// <summary>
        /// Returns a new <see cref="DateTime"/> that represents the last day in the instance month.
        /// </summary>
        /// <param name="instance">This instance.</param>
        /// <returns>New <see cref="DateTime"/> that represents the last day in the instance month.</returns>
        public static DateTime Last(this DateTime instance)
        {
            int daysInMonth = DateTime.DaysInMonth(instance.Year, instance.Month);
            return instance.AddDays(daysInMonth - instance.Day);
        }

        /// <summary>
        /// Returns a new <see cref="DateTime"/> that represents the last specified day in the instance month.
        /// </summary>
        /// <param name="instance">This instance.</param>
        /// <param name="dayOfWeek">The resultant day of week.</param>
        /// <returns>Last specified day in the instance month.</returns>
        public static DateTime Last(this DateTime instance, DayOfWeek dayOfWeek)
        {
            DateTime last = instance.Last();

            last = last.AddDays(-Math.Abs(dayOfWeek - last.DayOfWeek));
            return last;
        }

        /// <summary>
        /// Returns a new <see cref="DateTime"/> that represents the first date following the instance date which falls on the given day of the week.
        /// </summary>
        /// <param name="instance">This instance.</param>
        /// <param name="dayOfWeek">The day of week for the next date to get.</param>
        /// <returns>New <see cref="DateTime"/> that represents the first date following the instance date which falls on the given day of the week.</returns>
        public static DateTime Next(this DateTime instance, DayOfWeek dayOfWeek)
        {
            int offsetDays = dayOfWeek - instance.DayOfWeek;

            if (offsetDays <= 0)
            {
                offsetDays += DateTimeConstants.NumberOfDaysInWeek;
            }

            return instance.AddDays(offsetDays);
        }

        /// <summary>
        /// Returns a new <see cref="DateTime"/> that represents the next unit of time.
        /// </summary>
        /// <param name="instance">This instance.</param>
        /// <param name="dateType">The date unit (week, month, year, day).</param>
        /// <returns>New <see cref="DateTime"/> that represents the next unit of time.</returns>
        public static DateTime Next(this DateTime instance, DateType dateType)
        {
            switch (dateType)
            {
                case DateType.Day:
                    return instance.Date.AddDays(1);

                case DateType.Week:
                    return instance.Date.Next(CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);

                case DateType.Month:
                    return instance.Date.Last().AddDays(1);

                case DateType.Year:
                    return new DateTime(year: instance.Year + 1, month: 1, day: 1);

                default:
                    return instance.Date;
            }
        }

        /// <summary>
        /// Returns a new <see cref="DateTime"/> that represents the last unit of time.
        /// </summary>
        /// <param name="instance">This instance.</param>
        /// <param name="dateType">The date unit.</param>
        /// <returns>New <see cref="DateTime"/> that represents the last unit of time.</returns>
        public static DateTime Last(this DateTime instance, DateType dateType)
        {
            switch (dateType)
            {
                case DateType.Day:
                    return instance.Date.AddDays(-1);

                case DateType.Week:
                    return instance.Date.AddDays(-2 * DateTimeConstants.NumberOfDaysInWeek).Next(CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);

                case DateType.Month:
                    return instance.Date.First().AddDays(-1).First();

                case DateType.Year:
                    return new DateTime(year: instance.Year - 1, month: 1, day: 1);

                default:
                    return instance.Date;
            }
        }

        /// <summary>
        /// Returns a new <see cref="DateTime"/> that represents the instance unit of time.
        /// </summary>
        /// <param name="instance">This instance.</param>
        /// <param name="dateUnit">The date unit.</param>
        /// <returns>Current unit of time.</returns>
        public static DateTime This(this DateTime instance, DateType dateUnit)
        {
            switch (dateUnit)
            {
                case DateType.Day:
                    return instance.Date;

                case DateType.Week:
                    return instance.Date.AddWeeks(-1).Next(CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);

                case DateType.Month:
                    return instance.Date.First();

                case DateType.Year:
                    return new DateTime(year: instance.Year, month: 1, day: 1);

                default:
                    return instance.Date;
            }
        }

        /// <summary>
        /// Returns a new <see cref="DateTime"/> that adds the specified number of weeks to the value of this instance.
        /// </summary>
        /// <param name="instance">This instance.</param>
        /// <param name="value">A number of whole and fractional weeks. The value parameter can be negative or positive.</param>
        /// <returns>An object whose value is the sum of the date and time represented by this instance and the number of weeks represented by value.</returns>
        public static DateTime AddWeeks(this DateTime instance, double value)
        {
            return instance.AddDays(value * DateTimeConstants.NumberOfDaysInWeek);
        }

        /// <summary>
        /// Returns a new <see cref="DateTime"/> that represents midnight on the value of this instance.
        /// </summary>
        /// <param name="instance">This instance.</param>
        /// <returns>New <see cref="DateTime"/> that represents midnight on the value of this instance.</returns>
        public static DateTime Midnight(this DateTime instance)
        {
            return new DateTime(year: instance.Year, month: instance.Month, day: instance.Day, hour: 0, minute: 0, second: 0, millisecond: 0);
        }

        /// <summary>
        /// Returns a new <see cref="DateTime"/> that represents noon on the value of this instance.
        /// </summary>
        /// <param name="instance">This instance.</param>
        /// <returns>New <see cref="DateTime"/> that represents noon on the value of this instance.</returns>
        public static DateTime Noon(this DateTime instance)
        {
            return new DateTime(year: instance.Year, month: instance.Month, day: instance.Day, hour: 12, minute: 0, second: 0, millisecond: 0);
        }

        /// <summary>
        /// Returns a new <see cref="DateTime"/> with specified hour, and minute, and year, month, and day copied from this instance.
        /// </summary>
        /// <param name="instance">This instance.</param>
        /// <param name="hour">The hours (0 through 23).</param>
        /// <param name="minute">The minutes (0 through 59).</param>
        /// <returns>New <see cref="DateTime"/> with specified hour, and minute, and year, month, and day copied from this instance.</returns>
        public static DateTime SetTime(this DateTime instance, int hour, int minute)
        {
            return new DateTime(year: instance.Year, month: instance.Month, day: instance.Day, hour: hour, minute: minute, second: 0, millisecond: 0);
        }

        /// <summary>
        /// Returns a new <see cref="DateTime"/> with specified hour, minute, and second, and year, month, and day copied from this instance.
        /// </summary>
        /// <param name="instance">This instance.</param>
        /// <param name="hour">The hours (0 through 23).</param>
        /// <param name="minute">The minutes (0 through 59).</param>
        /// <param name="second">The seconds (0 through 59).</param>
        /// <returns>New <see cref="DateTime"/> with specified hour, minute, and second, and year, month, and day copied from this instance.</returns>
        public static DateTime SetTime(this DateTime instance, int hour, int minute, int second)
        {
            return new DateTime(year: instance.Year, month: instance.Month, day: instance.Day, hour: hour, minute: minute, second: second, millisecond: 0);
        }

        /// <summary>
        /// Returns a new <see cref="DateTime"/> with specified hour, minute, second, and millisecond, and year, month, and day copied from this instance.
        /// </summary>
        /// <param name="instance">This instance.</param>
        /// <param name="hour">The hours (0 through 23).</param>
        /// <param name="minute">The minutes (0 through 59).</param>
        /// <param name="second">The seconds (0 through 59).</param>
        /// <param name="millisecond">The milliseconds (0 through 999).</param>
        /// <returns>New <see cref="DateTime"/> with specified hour, minute, second, and millisecond, and year, month, and day copied from this instance.</returns>
        public static DateTime SetTime(this DateTime instance, int hour, int minute, int second, int millisecond)
        {
            return new DateTime(year: instance.Year, month: instance.Month, day: instance.Day, hour: hour, minute: minute, second: second, millisecond: millisecond);
        }

        /// <summary>
        /// Check whether the instance <see cref="DateTime"/> is in the same year with the reference.
        /// </summary>
        /// <param name="instance">This instance.</param>
        /// <param name="reference">The reference.</param>
        /// <returns>Are in the same year.</returns>
        public static bool IsInSameYearWith(this DateTime instance, DateTime reference)
        {
            return instance.Year == reference.Year;
        }

        /// <summary>
        /// Check whether the instance <see cref="DateTime"/> is in the same month with the reference.
        /// </summary>
        /// <param name="instance">This instance.</param>
        /// <param name="reference">The reference.</param>
        /// <returns>Are in the same month.</returns>
        public static bool IsInSameMonthWith(this DateTime instance, DateTime reference)
        {
            return (instance.Year == reference.Year) && (instance.Month == reference.Month);
        }

        /// <summary>
        /// Check whether the instance <see cref="DateTime"/> is in the same week with the reference.
        /// </summary>
        /// <param name="instance">This instance.</param>
        /// <param name="reference">The reference.</param>
        /// <returns>Are in the same week.</returns>
        public static bool IsInSameWeekWith(this DateTime instance, DateTime reference)
        {
            DateTime beginningOfWeek = instance.This(DateType.Week);
            return (beginningOfWeek - reference).TotalDays < DateTimeConstants.NumberOfDaysInWeek;
        }
    }
}
