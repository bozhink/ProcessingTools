// <copyright file="StringExtensions.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// Extension methods for <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Returns a new string in which all occurrences of a specified string in the current instance are replaced with another specified string or by regex pattern.
        /// </summary>
        /// <param name="input"> The string to search for a match.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <param name="replacement">The replacement string.</param>
        /// <param name="isRegex">Specifies if regex replacement has to be executed.</param>
        /// <returns>A new string that is identical to the input string, except that the replacement string takes the place of each matched string. If pattern is not matched in the current instance, the method returns the current instance unchanged.</returns>
        public static string Replace(this string input, string pattern, string replacement, bool isRegex)
        {
            if (isRegex)
            {
                return Regex.Replace(input: input, pattern: pattern, replacement: replacement);
            }
            else
            {
                return input.Replace(pattern, replacement);
            }
        }
    }
}
