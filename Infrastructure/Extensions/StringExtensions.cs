namespace ProcessingTools.Extensions
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        public static T Convert<T>(this string input)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter == null)
            {
                return default(T);
            }

            return (T)converter.ConvertFromString(input);
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
            var result = new HashSet<string>();

            for (Match m = search.Match(text); m.Success; m = m.NextMatch())
            {
                result.Add(m.Value);
            }

            return result;
        }
    }
}