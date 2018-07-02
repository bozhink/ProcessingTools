// <copyright file="DateTimeConstants.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Constants
{
    /// <summary>
    /// Date and time constants.
    /// </summary>
    public static class DateTimeConstants
    {
        /// <summary>
        /// Number of milliseconds in second.
        /// </summary>
        public const int NumberOfMillisecondsInSecond = 1000;

        /// <summary>
        /// Number of seconds in minute.
        /// </summary>
        public const int NumberOfSecondsInMinute = 60;

        /// <summary>
        /// Number of minutes in hour.
        /// </summary>
        public const int NumberOfMinutesInHour = 60;

        /// <summary>
        /// Number of hours in day.
        /// </summary>
        public const int NumberOfHoursInDay = 24;

        /// <summary>
        /// Number of days in week.
        /// </summary>
        public const int NumberOfDaysInWeek = 7;

        /// <summary>
        /// Number of hours in week.
        /// </summary>
        public const int NumberOfHoursInWeek = NumberOfDaysInWeek * NumberOfHoursInDay;

        /// <summary>
        /// Number of minutes in week.
        /// </summary>
        public const int NumberOfMinutesInWeek = NumberOfHoursInWeek * NumberOfMinutesInHour;

        /// <summary>
        /// Number of seconds in week.
        /// </summary>
        public const int NumberOfSecondsInWeek = NumberOfMinutesInWeek * NumberOfSecondsInMinute;

        /// <summary>
        /// Number of milliseconds in week.
        /// </summary>
        public const int NumberOfMillisecondsInWeek = NumberOfSecondsInWeek * NumberOfMillisecondsInSecond;
    }
}
