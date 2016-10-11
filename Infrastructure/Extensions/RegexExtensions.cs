namespace ProcessingTools.Extensions
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public static class RegexExtensions
    {
        public static IEnumerable<string> ToIEnumerable(this Match match)
        {
            for (var m = match; m.Success; m = m.NextMatch())
            {
                yield return m.Value;
            }
        }

        public static string RegexReplace(this string target, string regexPattern, string regexReplacement)
        {
            return Regex.Replace(target, regexPattern, regexReplacement);
        }

        public static string RegexReplace(this string target, Regex regex, string regexReplacement)
        {
            return regex.Replace(target, regexReplacement);
        }

        public static IEnumerable<string> GetMatches(this string text, Regex search)
        {
            return new HashSet<string>(search.Match(text).ToIEnumerable());
        }

        public static Task<IEnumerable<string>> GetMatchesAsync(this string text, Regex search)
        {
            return Task.Run(() =>
            {
                return text.GetMatches(search);
            });
        }
    }
}
