// <copyright file="UrlExtensions.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions
{
    using System.Net;

    /// <summary>
    /// URL Extensions.
    /// </summary>
    public static class UrlExtensions
    {
        /// <summary>
        /// Converts a text string into a URL-encoded string.
        /// </summary>
        /// <param name="text">The text to URL-encode.</param>
        /// <returns>Returns System.String.A URL-encoded string.</returns>
        public static string UrlEncode(this string text)
        {
            return WebUtility.UrlEncode(text).Replace("%20", "+");
        }

        /// <summary>
        /// Converts a string that has been encoded for transmission in a URL into a decoded string.
        /// </summary>
        /// <param name="text">A URL-encoded string to decode.</param>
        /// <returns>Returns System.String.A decoded string.</returns>
        public static string UrlDecode(this string text)
        {
            return WebUtility.UrlDecode(text);
        }
    }
}
