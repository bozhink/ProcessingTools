namespace ProcessingTools.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text.RegularExpressions;

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
