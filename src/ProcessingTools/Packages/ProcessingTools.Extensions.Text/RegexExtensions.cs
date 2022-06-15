// <copyright file="RegexExtensions.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Text
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Regex extensions.
    /// </summary>
    public static class RegexExtensions
    {
        /// <summary>
        /// <see cref="Match"/> as enumerable.
        /// </summary>
        /// <param name="match"><see cref="Match"/> to be evaluated.</param>
        /// <returns>Enumerable matches.</returns>
        public static IEnumerable<string> AsEnumerable(this Match match)
        {
            for (var m = match; m?.Success == true; m = m.NextMatch())
            {
                yield return m.Value;
            }
        }

        /// <summary>
        /// Regex replace.
        /// </summary>
        /// <param name="input">The string to search for a match.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <param name="replacement">The replacement string.</param>
        /// <returns>A new string that is identical to the input string, except that the replacement string takes the place of each matched string. If pattern is not matched in the current instance, the method returns the current instance unchanged.</returns>
        public static string RegexReplace(this string input, string pattern, string replacement)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(pattern))
            {
                return input;
            }

            return Regex.Replace(input: input, pattern: pattern, replacement: replacement);
        }

        /// <summary>
        /// Regex replace.
        /// </summary>
        /// <param name="input">The string to search for a match.</param>
        /// <param name="regex"><see cref="Regex"/> to match.</param>
        /// <param name="replacement">The replacement string.</param>
        /// <returns>A new string that is identical to the input string, except that the replacement string takes the place of each matched string. If pattern is not matched in the current instance, the method returns the current instance unchanged.</returns>
        public static string RegexReplace(this string input, Regex regex, string replacement)
        {
            if (string.IsNullOrEmpty(input) || regex is null)
            {
                return input;
            }

            return regex.Replace(input: input, replacement: replacement);
        }

        /// <summary>
        /// Gets all matches of <see cref="Regex"/> in input string.
        /// </summary>
        /// <param name="input">The string to search for a match.</param>
        /// <param name="regex"><see cref="Regex"/> to match.</param>
        /// <returns>Evaluated regex.</returns>
        public static IReadOnlyCollection<string> GetMatches(this string input, Regex regex)
        {
            if (string.IsNullOrEmpty(input))
            {
                return Array.Empty<string>();
            }

            if (regex is null)
            {
                return new[] { input };
            }

            return new HashSet<string>(regex.Match(input: input).AsEnumerable());
        }

        /// <summary>
        /// Gets all matches of <see cref="Regex"/> in input string.
        /// </summary>
        /// <param name="input">The string to search for a match.</param>
        /// <param name="regex"><see cref="Regex"/> to match.</param>
        /// <returns>Task of evaluated regex.</returns>
        public static Task<IReadOnlyCollection<string>> GetMatchesAsync(this string input, Regex regex)
        {
            if (string.IsNullOrEmpty(input))
            {
                return Task.FromResult<IReadOnlyCollection<string>>(Array.Empty<string>());
            }

            if (regex is null)
            {
                return Task.FromResult<IReadOnlyCollection<string>>(new[] { input });
            }

            return Task.Run(() => input.GetMatches(regex));
        }
    }
}
