namespace ProcessingTools.Data.Miners.Miners.Bio.Taxonomy
{
    using Contracts.Miners.Bio.Taxonomy;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class LowerTaxaDataMiner : ILowerTaxaDataMiner
    {
        private const string InfraRankSubpattern = @"(?:(?i)\b(?:subgen(?:us)?|subg|sg|(?:sub)?ser|trib|(?:super)?(?:sub)?sec[ct]?(?:ion)?|ab?|mod|sp|var|subvar|subsp|sbsp|subspec|subspecies|ssp|race|rassa|(?:sub)?f[ao]?|(?:sub)?forma?|st|r|sf|cf|gr|n\.?\s*sp|nr|(?:sp(?:\.\s*|\s+))?(?:near|afn|aff)|prope|(?:super)?(?:sub)?sec[ct]?(?:ion)?)\b\.?(?:\s*[γβɑ])?(?:\s*\bn(?:ova?)?\b\.?)?|×|\?)";

        public async Task<IEnumerable<string>> Mine(string content, IEnumerable<string> seed, IEnumerable<string> stopWords)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return new string[] { };
            }

            content = Regex.Replace(content, @"\s+", " ");
            string cleanedContent = this.CleanContent(content, stopWords);

            var extendedSeed = string.Join(" ", seed)
                .Split(new char[] { ' ', '(', ')' }, StringSplitOptions.RemoveEmptyEntries)
                .Distinct()
                .Concat(seed)
                .Distinct()
                .ToArray();

            var matches = new HashSet<string>();
            foreach (var item in extendedSeed)
            {
                var m = new Regex(@"(?i)" + item + @"(\s*.{0,30}?\s*" + InfraRankSubpattern + @"\s*\b[a-z](?:[a-z-]*\.|[a-z-]*[a-z]))*").Match(cleanedContent);
                if (m.Success)
                {
                    var value = m.Value.Trim();
                    if (value.Length > 1 && Regex.IsMatch(content, $"\\b{Regex.Escape(value)}\\b"))
                    {
                        matches.Add(m.Value);
                    }
                }
            }

            // TODO
            foreach (var item in matches.Distinct().OrderBy(i => i))
            {
                Console.WriteLine(item);
            }

            throw new NotImplementedException();
        }

        private string CleanContent(string content, IEnumerable<string> stopWords)
        {
            var words = this.CleanWords(content, stopWords);

            var cleanedContent = string.Join(" ", words);
            return cleanedContent;
        }

        private string[] CleanWords(string content, IEnumerable<string> stopWords)
        {
            var comparer = StringComparer.InvariantCultureIgnoreCase;

            var words = content.Split(new char[] { ' ', '\r', '\n', /*',',*/ ';', ':', '*', '/', '\\', '%', '@', '#', '=', '+', '!', '|', '<', '>' }, StringSplitOptions.RemoveEmptyEntries);

            var cleanedWords = new List<string>(words.Length);
            foreach (var word in words)
            {
                if (!stopWords.Contains(word, comparer))
                {
                    cleanedWords.Add(word);
                }
            }

            cleanedWords.TrimExcess();
            return cleanedWords.ToArray();
        }
    }
}
