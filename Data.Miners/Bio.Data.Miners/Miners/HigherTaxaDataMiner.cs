namespace ProcessingTools.Bio.Data.Miners
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Bio.Taxonomy.Constants;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Extensions;
    using ProcessingTools.Infrastructure.Extensions;

    public class HigherTaxaDataMiner : IHigherTaxaDataMiner
    {
        private readonly Regex matchHigherTaxa = new Regex(TaxaRegexPatterns.HigherTaxaMatchPattern);

        private readonly ITaxonRankDataService service;

        public HigherTaxaDataMiner(ITaxonRankDataService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.service = service;
        }

        public async Task<IQueryable<string>> Mine(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }

            string textToMine = string.Join(" ", content.ExtractWordsFromString());

            // Match plausible higher taxa by pattern.
            var items = new List<string>(await textToMine.GetMatchesAsync(this.matchHigherTaxa));

            items.AddRange(await this.MatchWithWhiteList(textToMine));

            var result = new HashSet<string>(items);
            return result.AsQueryable();
        }

        private async Task<IEnumerable<string>> MatchWithWhiteList(string textToMine)
        {
            var whiteListItems = new HashSet<string>((await this.service.All())
                .Where(t => t.IsWhiteListed)
                .Select(t => t.ScientificName));

            var whiteListMatches = textToMine.MatchWithStringList(whiteListItems, false, false, false);
            return whiteListMatches;
        }
    }
}