// <copyright file="DateTimeExtensions.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions
{
    using System;

    /// <summary>
    /// <see cref="DateTime"/> extensions.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Converts a given <see cref="DateTime"/> into a Unix timestamp.
        /// </summary>
        /// <param name="value"><see cref="DateTime"/> object.</param>
        /// <returns>The given <see cref="DateTime"/> in Unix timestamp format.</returns>
        /// <remarks>
        /// See https://stackoverflow.com/questions/17632584/how-to-get-the-unix-timestamp-in-c-sharp
        /// </remarks>
        public static int ToUnixTimestamp(this DateTime value)
        {
            return (int)Math.Truncate(value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
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
            return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(timestamp);
        }

        /// <summary>
        /// Gets a Unix timestamp representing the current moment
        /// </summary>
        /// <param name="ignored">Parameter ignored</param>
        /// <returns>Now expressed as a Unix timestamp</returns>
        /// <remarks>
        /// See https://stackoverflow.com/questions/17632584/how-to-get-the-unix-timestamp-in-c-sharp
        /// </remarks>
        public static int UnixTimestamp(this DateTime ignored)
        {
            return (int)Math.Truncate(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
        }
    }
}
