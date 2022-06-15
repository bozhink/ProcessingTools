// <copyright file="StringExtensions.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

// See https://github.com/RickStrahl/Westwind.AspNetCore/blob/master/Westwind.AspNetCore.Markdown/Utilities/StringUtils.cs
namespace ProcessingTools.Extensions.Text
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Extension methods for <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Match whitespace.
        /// </summary>
        public static readonly Regex MatchWhitespace = new Regex(@"\s+", RegexOptions.Compiled);

        /// <summary>
        /// Match character.
        /// </summary>
        public static readonly Regex MatchCharacter = new Regex(@"\w+", RegexOptions.Compiled);

        /// <summary>
        /// Match non-character.
        /// </summary>
        public static readonly Regex MatchNonCharacter = new Regex(@"\W+", RegexOptions.Compiled);

        /// <summary>
        /// Match new line symbols.
        /// </summary>
        public static readonly Regex MatchNewLine = new Regex(@"(\\r\\n|\r\n|\\r|\r|\\n|\n)", RegexOptions.Compiled);

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

        /// <summary>
        /// Extracts words from text.
        /// </summary>
        /// <param name="text">Text to be harvested.</param>
        /// <returns>Set of all words in text.</returns>
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

        /// <summary>
        /// Distinct words with stop words.
        /// </summary>
        /// <param name="words">Words to be processed.</param>
        /// <param name="stopWords">Stop words to be applied.</param>
        /// <returns>Cleaned words.</returns>
        public static IEnumerable<string> DistinctWithStopWords(this IEnumerable<string> words, IEnumerable<string> stopWords)
        {
            if (words is null)
            {
                return Array.Empty<string>();
            }

            if (stopWords is null)
            {
                return words;
            }

            var compareSet = new HashSet<string>(stopWords.Select(w => w.ToUpperInvariant()));
            if (compareSet.Count < 1)
            {
                return words;
            }

            var result = new HashSet<string>(words.Where(w => !compareSet.Contains(w.ToUpperInvariant())));
            return result;
        }

        /// <summary>
        /// Match words with seed words.
        /// </summary>
        /// <param name="words">Words to be processed.</param>
        /// <param name="seed">Seed words to be applied.</param>
        /// <returns>Matched words.</returns>
        public static IEnumerable<string> MatchWithSeedWords(this IEnumerable<string> words, IEnumerable<string> seed)
        {
            if (words is null || seed is null)
            {
                return Array.Empty<string>();
            }

            var compareSet = new HashSet<string>(seed.Select(w => w.ToUpperInvariant()));
            if (compareSet.Count < 1)
            {
                return Array.Empty<string>();
            }

            var result = new HashSet<string>(words.Where(w => compareSet.Contains(w.ToUpperInvariant())));
            return result;
        }

        /// <summary>
        /// Makes the first letter of a text upper-case and all other letters in lower-case.
        /// </summary>
        /// <param name="text">Text to be transformed.</param>
        /// <returns>Transformed text.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "ToLower")]
        public static string ToFirstLetterUpperCase(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            int length = text.Length;
            if (length > 1)
            {
                return text[0..1].ToUpperInvariant() + text[1..length].ToLowerInvariant();
            }

            return text.ToUpperInvariant();
        }

        /// <summary>
        /// Map string content to stream.
        /// </summary>
        /// <param name="content">Text content to be streamed.</param>
        /// <returns>Stream of the content.</returns>
        public static Stream ToStream(this string content)
        {
            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            Stream stream;

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

        /// <summary>
        /// Extracts a string from between a pair of delimiters. Only the first instance is found.
        /// </summary>
        /// <param name="source">Input String to work on.</param>
        /// <param name="beginingDelimiter">Beginning delimiter.</param>
        /// <param name="endingDelimiter">Ending delimiter.</param>
        /// <param name="caseSensitive">Determines whether the search for delimiters is case sensitive.</param>
        /// <param name="allowMissingEndingDelimiter">Determines whether is allowed the ending delimiter to be missing.</param>
        /// <param name="returnDelimiters">Determines whether delimiters have to be present in the result.</param>
        /// <returns>Extracted string or "".</returns>
        public static string ExtractString(this string source, string beginingDelimiter, string endingDelimiter, bool caseSensitive = false, bool allowMissingEndingDelimiter = false, bool returnDelimiters = false)
        {
            if (string.IsNullOrEmpty(source))
            {
                return string.Empty;
            }

            string beginingDelimiterWrapped = $"{beginingDelimiter}";
            string endingDelimiterWrapped = $"{endingDelimiter}";

            StringComparison comparisonType = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            int beginingDelimiterIndex = source.IndexOf(beginingDelimiterWrapped, 0, source.Length, comparisonType);
            if (beginingDelimiterIndex == -1)
            {
                return string.Empty;
            }

            int endingDelimiterIndex = source.IndexOf(endingDelimiterWrapped, beginingDelimiterIndex + beginingDelimiterWrapped.Length, comparisonType);

            if (allowMissingEndingDelimiter && endingDelimiterIndex < 0)
            {
                if (!returnDelimiters)
                {
                    return source.Substring(beginingDelimiterIndex + beginingDelimiterWrapped.Length);
                }

                return source.Substring(beginingDelimiterIndex);
            }

            if (beginingDelimiterIndex > -1 && endingDelimiterIndex > 1)
            {
                if (!returnDelimiters)
                {
                    return source.Substring(beginingDelimiterIndex + beginingDelimiterWrapped.Length, endingDelimiterIndex - beginingDelimiterIndex - beginingDelimiterWrapped.Length);
                }

                return source.Substring(beginingDelimiterIndex, endingDelimiterIndex - beginingDelimiterIndex + endingDelimiterWrapped.Length);
            }

            return string.Empty;
        }

        /// <summary>
        /// Replaces a substring within a string with another substring with optional case sensitivity turned off.
        /// </summary>
        /// <param name="originalString">String to do replacements on.</param>
        /// <param name="findString">The string to find.</param>
        /// <param name="replaceString">The string to replace found string with.</param>
        /// <param name="caseInsensitive">If true case insensitive search is performed.</param>
        /// <returns>Updated string or original string if no matches.</returns>
        public static string ReplaceString(this string originalString, string findString, string replaceString, bool caseInsensitive)
        {
            if (string.IsNullOrEmpty(originalString) || string.IsNullOrEmpty(findString))
            {
                return originalString;
            }

            int findStringIndex = 0;
            while (true)
            {
                if (caseInsensitive)
                {
                    findStringIndex = originalString.IndexOf(findString, findStringIndex, originalString.Length - findStringIndex, StringComparison.InvariantCultureIgnoreCase);
                }
                else
                {
                    findStringIndex = originalString.IndexOf(findString, findStringIndex, StringComparison.InvariantCulture);
                }

                if (findStringIndex == -1)
                {
                    break;
                }

                originalString = originalString.Substring(0, findStringIndex) + replaceString + originalString.Substring(findStringIndex + findString.Length);

                findStringIndex += replaceString?.Length ?? 0;
            }

            return originalString;
        }

        /// <summary>
        /// Parses a string into an array of non-empty lines split by new line.
        /// </summary>
        /// <param name="source">String to be split.</param>
        /// <returns>Array of non-empty lines.</returns>
        public static string[] GetNonEmptyLines(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return Array.Empty<string>();
            }

            return MatchNewLine.Replace(source, "\n").Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Parses a string into an array of lines split by new line.
        /// </summary>
        /// <param name="source">String to be split.</param>
        /// <returns>Array of lines.</returns>
        public static string[] GetLines(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return Array.Empty<string>();
            }

            return MatchNewLine.Replace(source, "\n").Split(new[] { '\n' }, StringSplitOptions.None);
        }

        /// <summary>
        /// Truncates source string on a specified length.
        /// </summary>
        /// <param name="source">Source string to be truncated.</param>
        /// <param name="length">Length of the resultant string.</param>
        /// <returns>Truncated string.</returns>
        public static string TruncateOn(this string source, int length)
        {
            if (string.IsNullOrEmpty(source) || source.Length <= length)
            {
                return source;
            }
            else
            {
                return source.Substring(0, length);
            }
        }

        /// <summary>
        /// Truncates source string on a specified length with adding ellipsis on the end.
        /// </summary>
        /// <param name="source">Source string to be truncated.</param>
        /// <param name="length">Length of the resultant string.</param>
        /// <returns>Truncated string.</returns>
        public static string TruncateWithEllipsisOn(this string source, int length) => TruncateWithEllipsisOn(source, length, " ...");

        /// <summary>
        /// Truncates source string on a specified length with adding ellipsis on the end.
        /// </summary>
        /// <param name="source">Source string to be truncated.</param>
        /// <param name="length">Length of the resultant string.</param>
        /// <param name="ellipsis">Ellipsis string.</param>
        /// <returns>Truncated string.</returns>
        public static string TruncateWithEllipsisOn(this string source, int length, string ellipsis)
        {
            string ellipsisString = ellipsis ?? string.Empty;
            int bodyLength = length - ellipsisString.Length;

            if (string.IsNullOrEmpty(source) || source.Length <= bodyLength)
            {
                return source;
            }
            else
            {
                return source.Substring(0, bodyLength) + ellipsisString;
            }
        }

        /// <summary>
        /// Trim multiple white-spaces on a source string.
        /// </summary>
        /// <param name="source">Source string to be processed.</param>
        /// <returns>Processed string.</returns>
        public static string TrimMultiWhitespaces(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return source;
            }

            return MatchWhitespace.Replace(source, " ").Trim();
        }

        /// <summary>
        /// Replace DateTime wildcard in the source string.
        /// </summary>
        /// <param name="source">Source string to be processed.</param>
        /// <param name="dateTime">DateTime value to be applied.</param>
        /// <returns>Resultant string with DateTime wildcard processed.</returns>
        public static string ReplaceDateTimeWildcard(this string source, DateTime dateTime)
        {
            string pattern = @"(?i)\[DateTime\(([^\(\)]+)\)\]";

            StringBuilder sb = new StringBuilder();
            int i = 0;
            for (Match m = Regex.Match(source, pattern); m.Success; m = m.NextMatch())
            {
                string matchValue = m.Value;
                string formatString = m.Groups[1].Value;

                int index = source.IndexOf(matchValue, i, StringComparison.InvariantCultureIgnoreCase);
                string text = source[i..index];

                sb.Append(text);
                sb.Append(dateTime.ToString(formatString, CultureInfo.InvariantCulture));

                i = index + matchValue.Length;
            }

            sb.Append(source.Substring(i));

            return sb.ToString();
        }

        /// <summary>
        /// Encode non-ASCII characters.
        /// </summary>
        /// <param name="source">Source string to be encoded.</param>
        /// <returns>Encoded string as ASCII.</returns>
        /// <remarks>
        /// See https://stackoverflow.com/questions/1615559/convert-a-unicode-string-to-an-escaped-ascii-string.
        /// </remarks>
        public static string EncodeNonAsciiCharacters(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return source;
            }

            StringBuilder sb = new StringBuilder();
            foreach (char c in source)
            {
                if (c > 127)
                {
                    // This character is too big for ASCII
                    string encodedValue = "\\u" + ((int)c).ToString("x4", CultureInfo.InvariantCulture);
                    sb.Append(encodedValue);
                }
                else
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Decode encoded non-ASCII characters.
        /// </summary>
        /// <param name="source">Source string to be decoded.</param>
        /// <returns>Decoded string as Unicode.</returns>
        /// <remarks>
        /// See https://stackoverflow.com/questions/1615559/convert-a-unicode-string-to-an-escaped-ascii-string.
        /// </remarks>
        public static string DecodeEncodedNonAsciiCharacters(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return source;
            }

            return Regex.Replace(
                source,
                @"\\u(?<Value>[a-zA-Z0-9]{4})",
                m =>
                {
                    return ((char)int.Parse(m.Groups["Value"].Value, NumberStyles.HexNumber, CultureInfo.InvariantCulture)).ToString(CultureInfo.InvariantCulture);
                });
        }

        /// <summary>
        /// Clean names to invariant form.
        /// </summary>
        /// <param name="names">List of string names to be cleaned.</param>
        /// <returns>Cleaned names to invariant form.</returns>
        public static IEnumerable<string> CleanNamesToInvariant(this IEnumerable<string> names)
        {
            if (names is null || !names.Any())
            {
                return Array.Empty<string>();
            }

            return new HashSet<string>(names.Select(s => MatchWhitespace.Replace(s, " ").Trim())
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => s.ToUpperInvariant()));
        }

        /// <summary>
        /// Gets all strings which does not contain any string of a comparison list.
        /// </summary>
        /// <param name="wordList">IEnumerable object to be distinct.</param>
        /// <param name="compareList">IEnumerable object of patterns which must not be contained in the wordList.</param>
        /// <param name="treatAsRegex">Treat compareList items as regex patterns or not.</param>
        /// <param name="caseSensitive">Perform case-sensitive search or not.</param>
        /// <param name="strictMode">Specify if whole-phrase-match is expected.</param>
        /// <returns>IEnumerable object of all non-matching with compareList string items in wordList.</returns>
        public static IEnumerable<string> DistinctWithStringList(
            this IEnumerable<string> wordList,
            IEnumerable<string> compareList,
            bool treatAsRegex = false,
            bool caseSensitive = false,
            bool strictMode = false)
        {
            var list = new HashSet<string>(wordList);
            var result = from word in list
                         where !word.MatchWithStringList(compareList, treatAsRegex, caseSensitive, strictMode).Any()
                         select word;

            return new HashSet<string>(result);
        }

        /// <summary>
        /// Gets all items of an IEnumerable object which are contained in the given string.
        /// </summary>
        /// <param name="word">The string which should contain some items of interest.</param>
        /// <param name="compareList">IEnumerable object which items provides the output items.</param>
        /// <param name="treatAsRegex">Treat compareList items as regex patterns or not.</param>
        /// <param name="caseSensitive">Perform case-sensitive search or not.</param>
        /// <param name="strictMode">Specify if whole-phrase-match is expected.</param>
        /// <returns>IEnumerable object of all matching string items of compareList in word.</returns>
        /// <remarks>If match-whole-word-search is needed treatAsRegex should be set to true, and values in compareList should be Regex.Escape-d if needed.</remarks>
        public static IEnumerable<string> MatchWithStringList(
            this string word,
            IEnumerable<string> compareList,
            bool treatAsRegex = false,
            bool caseSensitive = false,
            bool strictMode = false)
        {
            IEnumerable<string> result = null;

            var list = new HashSet<string>(compareList);
            if (strictMode)
            {
                if (treatAsRegex)
                {
                    if (caseSensitive)
                    {
                        result = list.Where(c => Regex.IsMatch(word, $"\\A{c}\\Z"));
                    }
                    else
                    {
                        result = list.Where(c => Regex.IsMatch(word, $"\\A(?i){c}\\Z"));
                    }
                }
                else
                {
                    if (caseSensitive)
                    {
                        result = list.Where(c => string.Equals(word, c, StringComparison.InvariantCulture));
                    }
                    else
                    {
                        result = list.Where(c => string.Equals(word, c, StringComparison.InvariantCultureIgnoreCase));
                    }
                }
            }
            else
            {
                if (treatAsRegex)
                {
                    if (caseSensitive)
                    {
                        result = from comparePattern in list
                                 where Regex.IsMatch(word, @"\b" + comparePattern + @"\b")
                                 select comparePattern;
                    }
                    else
                    {
                        result = from comparePattern in list
                                 where Regex.IsMatch(word, @"\b(?i)" + comparePattern + @"\b")
                                 select comparePattern;
                    }
                }
                else
                {
                    if (caseSensitive)
                    {
                        result = from stringToCompare in list
                                 where word.Contains(stringToCompare, StringComparison.InvariantCulture)
                                 select stringToCompare;
                    }
                    else
                    {
                        result = from stringToCompare in list
                                 where word.Contains(stringToCompare, StringComparison.InvariantCultureIgnoreCase)
                                 select stringToCompare;
                    }
                }
            }

            return new HashSet<string>(result);
        }

        /// <summary>
        /// Gets all strings which contains any string of a comparison list.
        /// </summary>
        /// <param name="wordList">IEnumerable object to be matches.</param>
        /// <param name="compareList">IEnumerable object of patterns which must be contained in the wordList.</param>
        /// <param name="treatAsRegex">Treat compareList items as regex patterns or not.</param>
        /// <param name="caseSensitive">Perform case-sensitive search or not.</param>
        /// <param name="strictMode">Specify if whole-phrase-match is expected.</param>
        /// <returns>IEnumerable object of all matching with compareList string items in wordList.</returns>
        public static IEnumerable<string> MatchWithStringList(
            this IEnumerable<string> wordList,
            IEnumerable<string> compareList,
            bool treatAsRegex = false,
            bool caseSensitive = false,
            bool strictMode = false)
        {
            var list = new HashSet<string>(compareList);
            var result = from word in wordList
                         where word.MatchWithStringList(list, treatAsRegex, caseSensitive, strictMode).Any()
                         select word;

            return new HashSet<string>(result);
        }
    }
}
