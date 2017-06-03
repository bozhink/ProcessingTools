namespace ProcessingTools.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using ProcessingTools.Constants;

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
            if (string.IsNullOrWhiteSpace(text))
            {
                return result;
            }

            for (Match word = matchWord.Match(text); word.Success; word = word.NextMatch())
            {
                result.Add(word.Value);
            }

            return result;
        }

        public static IEnumerable<string> DistinctWithStopWords(this IEnumerable<string> words, IEnumerable<string> stopWords)
        {
            if (words == null)
            {
                return new string[] { };
            }

            if (stopWords == null)
            {
                return words;
            }

            var compareSet = new HashSet<string>(stopWords.Select(w => w.ToLower()));
            if (compareSet.Count < 1)
            {
                return words;
            }

            var result = new HashSet<string>(words.Where(w => !compareSet.Contains(w.ToLower())));
            return result;
        }

        public static IEnumerable<string> MatchWithSeedWords(this IEnumerable<string> words, IEnumerable<string> seed)
        {
            if (words == null || seed == null)
            {
                return new string[] { };
            }

            var compareSet = new HashSet<string>(seed.Select(w => w.ToLower()));
            if (compareSet.Count < 1)
            {
                return new string[] { };
            }

            var result = new HashSet<string>(words.Where(w => compareSet.Contains(w.ToLower())));
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

        public static Stream ToStream(this string content)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            Stream stream = null;

            try
            {
                byte[] bytesContent = Defaults.Encoding.GetBytes(content);
                stream = new MemoryStream(bytesContent);
            }
            catch (EncoderFallbackException e)
            {
                throw new EncoderFallbackException($"Input document string should be {Defaults.Encoding.EncodingName} encoded.", e);
            }

            return stream;
        }
    }
}
