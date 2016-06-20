namespace ProcessingTools.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class ListIntersectionsExtensions
    {
        /// <summary>
        /// Gets all strings which does not contain any string of a comparison list.
        /// </summary>
        /// <param name="wordList">IEnumerable object to be distincted.</param>
        /// <param name="compareList">IEnumerable object of patterns which must not be contained in the wordList.</param>
        /// <param name="caseSensitive">Perform case-sensitive search or not.</param>
        /// <param name="treatAsRegex">Treat compareList items as regex patterns or not.</param>
        /// <param name="strictMode">Specify if whole-phrase-match is expected.</param>
        /// <returns>IEnumerable object of all non-matching with compareList string items in wordList.</returns>
        public static IEnumerable<string> DistinctWithStringList(
            this IEnumerable<string> wordList,
            IEnumerable<string> compareList,
            bool treatAsRegex = false,
            bool caseSensitive = false,
            bool strictMode = false)
        {
            try
            {
                var list = new HashSet<string>(wordList);
                var result = from word in list
                             where word.MatchWithStringList(compareList, treatAsRegex, caseSensitive, strictMode).Count() == 0
                             select word;

                return new HashSet<string>(result);
            }
            catch
            {
                throw;
            }
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
            try
            {
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
                            result = list.Where(c => word == c);
                        }
                        else
                        {
                            string wordLowerCase = word.ToLower();
                            result = list
                                .Select(c => c.ToLower())
                                .Where(c => wordLowerCase == c);
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
                                     where word.Contains(stringToCompare)
                                     select stringToCompare;
                        }
                        else
                        {
                            string wordLowerCase = word.ToLower();
                            result = from stringToCompare in list
                                     where wordLowerCase.Contains(stringToCompare.ToLower())
                                     select stringToCompare;
                        }
                    }
                }
            }
            catch
            {
                throw;
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
            try
            {
                var list = new HashSet<string>(compareList);
                var result = from word in wordList
                             where word.MatchWithStringList(list, treatAsRegex, caseSensitive, strictMode).Count() > 0
                             select word;

                return new HashSet<string>(result);
            }
            catch
            {
                throw;
            }
        }
    }
}