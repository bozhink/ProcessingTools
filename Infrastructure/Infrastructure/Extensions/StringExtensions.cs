namespace ProcessingTools.Infrastructure.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public static class StringExtensions
    {
        private static readonly Regex MatchWord = new Regex(@"[^\W\d]+");

        public static T ConvertTo<T>(this string input)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter == null)
            {
                return default(T);
            }

            return (T)converter.ConvertFromString(input);
        }

        public static object ConvertTo(this string input, Type type)
        {
            var converter = TypeDescriptor.GetConverter(type);
            if (converter == null)
            {
                return type.Default();
            }

            return converter.ConvertFromString(input);
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

        public static Task<IEnumerable<string>> GetMatchesAsync(this string text, Regex search)
        {
            return Task.Run(() =>
            {
                return text.GetMatches(search);
            });
        }

        public static IEnumerable<string> ExtractWordsFromString(this string text)
        {
            var result = new HashSet<string>();
            for (Match word = MatchWord.Match(text); word.Success; word = word.NextMatch())
            {
                result.Add(word.Value);
            }

            return result;
        }

        /// <summary>
        /// Makes the first letter of a text upper-case and all other letters in lower-case.
        /// </summary>
        /// <param name="text">Text to be transformed.</param>
        /// <returns>Transformed text.</returns>
        public static string ToFirstLetterUpperCase(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            int length = text.Length;
            return text.Substring(0, 1).ToUpper() + text.Substring(1, length - 1).ToLower();
        }
    }
}