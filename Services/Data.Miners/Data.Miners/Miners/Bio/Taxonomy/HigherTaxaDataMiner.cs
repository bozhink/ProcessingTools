namespace ProcessingTools.Data.Miners.Miners.Bio.Taxonomy
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Contracts.Miners.Bio.Taxonomy;
    using ProcessingTools.Bio.Taxonomy.Constants;
    using ProcessingTools.Extensions.Linq;
    using ProcessingTools.Strings.Extensions;

    public class HigherTaxaDataMiner : IHigherTaxaDataMiner
    {
        private readonly Regex matchHigherTaxa = new Regex(TaxaRegexPatterns.HigherTaxaMatchPattern);

        public async Task<IEnumerable<string>> Mine(string content, IEnumerable<string> seed, IEnumerable<string> stopWords)
        {
            var result = new HashSet<string>();

            if (string.IsNullOrWhiteSpace(content))
            {
                return result;
            }

            var words = await content.ExtractWordsFromText()
                .DistinctWithStopWords(stopWords)
                .MatchWithSeedWords(seed)
                .ToArrayAsync();

            return new HashSet<string>(words);
        }
    }
}
