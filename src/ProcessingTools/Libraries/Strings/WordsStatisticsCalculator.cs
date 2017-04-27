namespace ProcessingTools.Strings
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class WordsStatisticsCalculator
    {
        public IDictionary<string, ulong> CalculateWordFrequency(IEnumerable<string> words)
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

        public IEnumerable<string> ExtractWords(string text, bool preserveNumbers = true, bool distictWords = false, bool changeToLowerCase = false)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return new string[] { };
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

                if (changeToLowerCase)
                {
                    words.Add(word.ToLower());
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
