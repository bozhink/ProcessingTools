// <copyright file="UriExtensions.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions
{
    using System;
    using System.Linq;

    /// <summary>
    /// <see cref="Uri"/> extensions.
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Append path value to a URI.
        /// </summary>
        /// <param name="uri">Base URI.</param>
        /// <param name="path">Relative path to be appended.</param>
        /// <returns>Resultant <see cref="Uri"/> object.</returns>
        public static Uri Append(string uri, string path)
        {
            return new Uri($"{uri?.TrimEnd('/')}/{path?.TrimStart('/')}");
        }

        /// <summary>
        /// Append path value to a URI.
        /// </summary>
        /// <param name="uri">Base URI.</param>
        /// <param name="path">Relative path to be appended.</param>
        /// <returns>Resultant <see cref="Uri"/> object.</returns>
        public static Uri Append(this Uri uri, string path)
        {
            return new Uri($"{uri?.ToString().TrimEnd('/')}/{path?.TrimStart('/')}");
        }

        /// <summary>
        /// Append path values to a URI.
        /// </summary>
        /// <param name="uri">Base URI.</param>
        /// <param name="paths">Relative paths to be appended.</param>
        /// <returns>Resultant <see cref="Uri"/> object.</returns>
        // See https://stackoverflow.com/questions/372865/path-combine-for-urls
        public static Uri Append(string uri, params string[] paths)
        {
            return new Uri(paths.Aggregate(uri, (current, path) => $"{current?.TrimEnd('/')}/{path?.TrimStart('/')}"));
        }

        /// <summary>
        /// Append path values to a URI.
        /// </summary>
        /// <param name="uri">Base URI.</param>
        /// <param name="paths">Relative paths to be appended.</param>
        /// <returns>Resultant <see cref="Uri"/> object.</returns>
        // See https://stackoverflow.com/questions/372865/path-combine-for-urls
        public static Uri Append(this Uri uri, params string[] paths)
        {
            return new Uri(paths.Aggregate(uri.AbsoluteUri, (current, path) => $"{current?.TrimEnd('/')}/{path?.TrimStart('/')}"));
        }
    }
}
