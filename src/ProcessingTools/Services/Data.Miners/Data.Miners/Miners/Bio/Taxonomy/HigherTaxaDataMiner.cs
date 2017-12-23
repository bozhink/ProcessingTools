namespace ProcessingTools.Data.Miners.Miners.Bio.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Constants;
    using ProcessingTools.Data.Miners.Contracts.Miners.Bio.Taxonomy;
    using ProcessingTools.Extensions;

    public class HigherTaxaDataMiner : IHigherTaxaDataMiner
    {
        private readonly Regex matchHigherTaxa = new Regex(TaxaRegexPatterns.HigherTaxaMatchPattern);

        public Task<string[]> MineAsync(string context, IEnumerable<string> seed, IEnumerable<string> stopWords)
        {
            return Task.Run(() =>
            {
                if (string.IsNullOrWhiteSpace(context))
                {
                    return new string[] { };
                }

                var words = context.ExtractWordsFromText()
                    .DistinctWithStopWords(stopWords)
                    .ToArray();

                var result = words.Where(w => this.matchHigherTaxa.IsMatch(w))
                    .Union(words.MatchWithSeedWords(seed))
                    .ToArray();

                return new HashSet<string>(result).ToArray();
            });
        }
    }
}
