namespace ProcessingTools.Data.Miners.Miners.Bio.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Contracts.Miners.Bio.Taxonomy;
    using ProcessingTools.Constants;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Common.Extensions.Linq;

    public class HigherTaxaDataMiner : IHigherTaxaDataMiner
    {
        private readonly Regex matchHigherTaxa = new Regex(TaxaRegexPatterns.HigherTaxaMatchPattern);

        public async Task<IEnumerable<string>> Mine(string content, IEnumerable<string> seed, IEnumerable<string> stopWords)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return new string[] { };
            }

            var words = await content.ExtractWordsFromText()
                .DistinctWithStopWords(stopWords)
                .ToArrayAsync();

            var result = await words.Where(w => this.matchHigherTaxa.IsMatch(w))
                .Union(words.MatchWithSeedWords(seed))
                .ToArrayAsync();

            return new HashSet<string>(result);
        }
    }
}
