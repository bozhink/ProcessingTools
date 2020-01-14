﻿// <copyright file="UrlExtensions.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

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
        public static string UrlEncode(this string text) => WebUtility.UrlEncode(text).Replace("%20", "+");

        /// <summary>
        /// Converts a string that has been encoded for transmission in a URL into a decoded string.
        /// </summary>
        /// <param name="text">A URL-encoded string to decode.</param>
        /// <returns>Returns System.String.A decoded string.</returns>
        public static string UrlDecode(this string text) => WebUtility.UrlDecode(text);

        /// <summary>
        /// Get query string from dictionary of query parameters.
        /// </summary>
        /// <param name="queryParameters">Dictionary of query parameters.</param>
        /// <returns>URL encoded payload for query string.</returns>
        public static async Task<string> GetQueryStringAsync(this IDictionary<string, string> queryParameters)
        {
            if (queryParameters is null || queryParameters.Count < 1)
            {
                return null;
            }

            using (var content = new FormUrlEncodedContent(queryParameters))
            {
                return await content.ReadAsStringAsync().ConfigureAwait(false);
            }
        }
    }
}
