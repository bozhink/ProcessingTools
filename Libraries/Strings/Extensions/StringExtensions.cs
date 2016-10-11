namespace ProcessingTools.Strings.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        /// <summary>
        /// Gets list of first words of a given list of strings.
        /// </summary>
        /// <param name="phrases">IEnumerable&lt;string&gt; object from which to extract first words.</param>
        /// <returns>IEnumerable&lt;string&gt; object containing every first word in the input list.</returns>
        public static IEnumerable<string> GetFirstWord(this IEnumerable<string> phrases)
        {
            return new HashSet<string>(phrases.Select(phrase => phrase.GetFirstWord()));
        }

        /// <summary>
        /// Gets the first word of a given string.
        /// </summary>
        /// <param name="phrase">String from which to extract the first word.</param>
        /// <returns>String of the first word.</returns>
        public static string GetFirstWord(this string phrase)
        {
            var matchFirstWord = new Regex(@"\A(?:[^\W\d_]{1,3}\.|[^\W\d_]{2,})");
            var firstWord = matchFirstWord.Match(phrase).Value;
            return firstWord;
        }

        public static IEnumerable<string> ExtractWordsFromText(this string text)
        {
            var matchWord = new Regex(@"[^\W\d]+");

            var result = new HashSet<string>();
            for (Match word = matchWord.Match(text); word.Success; word = word.NextMatch())
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
            if (string.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            int length = text.Length;
            return text.Substring(0, 1).ToUpper() + text.Substring(1, length - 1).ToLower();
        }
    }
}
