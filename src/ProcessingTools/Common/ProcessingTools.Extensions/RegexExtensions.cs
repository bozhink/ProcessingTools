// <copyright file="RegexExtensions.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
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
            for (var m = match; m.Success; m = m.NextMatch())
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
            return regex.Replace(input: input, replacement: replacement);
        }

        /// <summary>
        /// Gets all matches of <see cref="Regex"/> in input string.
        /// </summary>
        /// <param name="input">The string to search for a match.</param>
        /// <param name="regex"><see cref="Regex"/> to match.</param>
        /// <returns>Evaluated regex.</returns>
        public static string[] GetMatches(this string input, Regex regex)
        {
            return new HashSet<string>(regex.Match(input: input).AsEnumerable()).ToArray();
        }

        /// <summary>
        /// Gets all matches of <see cref="Regex"/> in input string.
        /// </summary>
        /// <param name="input">The string to search for a match.</param>
        /// <param name="regex"><see cref="Regex"/> to match.</param>
        /// <returns>Task of evaluated regex.</returns>
        public static Task<string[]> GetMatchesAsync(this string input, Regex regex)
        {
            return Task.Run(() => input.GetMatches(regex));
        }
    }
}
