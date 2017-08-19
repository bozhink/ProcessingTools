namespace ProcessingTools.Common.Extensions
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public static class RegexExtensions
    {
        public static IEnumerable<string> AsEnumerable(this Match match)
        {
            for (var m = match; m.Success; m = m.NextMatch())
            {
                yield return m.Value;
            }
        }

        public static string RegexReplace(this string input, string pattern, string replacement)
        {
            return Regex.Replace(input: input, pattern: pattern, replacement: replacement);
        }

        public static string RegexReplace(this string input, Regex regex, string replacement)
        {
            return regex.Replace(input: input, replacement: replacement);
        }

        public static IEnumerable<string> GetMatches(this string input, Regex regex)
        {
            return new HashSet<string>(regex.Match(input: input).AsEnumerable());
        }

        public static Task<IEnumerable<string>> GetMatchesAsync(this string text, Regex regex)
        {
            return Task.Run(() =>
            {
                return text.GetMatches(regex);
            });
        }
    }
}
