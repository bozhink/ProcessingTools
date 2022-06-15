// <copyright file="WordsStatisticsCalculator.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Text
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Words statistics calculator.
    /// </summary>
    public class WordsStatisticsCalculator
    {
        /// <summary>
        /// Calculated word frequency.
        /// </summary>
        /// <param name="words">Collection of words to be evaluated.</param>
        /// <returns>Frequency distribution of the words in the collection.</returns>
        public IReadOnlyDictionary<string, ulong> CalculateWordFrequency(IEnumerable<string> words)
        {
            var result = new Dictionary<string, ulong>();

            if (words != null)
            {
                foreach (var word in words)
                {
                    if (result.ContainsKey(word))
                    {
                        result[word]++;
                    }
                    else
                    {
                        result[word] = 1L;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Extracts words from specified text.
        /// </summary>
        /// <param name="text">Text as string to be processed.</param>
        /// <param name="preserveNumbers">Whether numbers should be preserved.</param>
        /// <param name="distictWords">Whether only unique words should be outputted.</param>
        /// <param name="changeToUpperCase">Whether words should be changed to uppercase.</param>
        /// <returns>Collection of found words.</returns>
        public IEnumerable<string> ExtractWords(string text, bool preserveNumbers = true, bool distictWords = false, bool changeToUpperCase = false)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return Array.Empty<string>();
            }

            string matchWordsPattern = @"\w+";
            if (!preserveNumbers)
            {
                matchWordsPattern = @"[^\W\d]+";
            }

            ICollection<string> words;
            if (distictWords)
            {
                words = new HashSet<string>();
            }
            else
            {
                words = new List<string>();
            }

            for (var m = Regex.Match(text, matchWordsPattern); m.Success; m = m.NextMatch())
            {
                var word = m.Value.Trim();

                if (changeToUpperCase)
                {
                    words.Add(word.ToUpperInvariant());
                }
                else
                {
                    words.Add(word);
                }
            }

            return words;
        }
    }
}
