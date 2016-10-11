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
    using ProcessingTools.Strings.Extensions;

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

            var words = content.ExtractWordsFromText();

            var matches = words.Where(w => this.matchHigherTaxa.IsMatch(w))
                .ToList();

            matches.AddRange(await this.MatchWithWhiteList(words));

            var result = new HashSet<string>(matches);
            return result.AsQueryable();
        }

        private async Task<IEnumerable<string>> MatchWithWhiteList(IEnumerable<string> words)
        {
            var whiteListItems = new HashSet<string>((await this.service.GetWhiteListedTaxa())
                .Select(t => t.ScientificName.ToLower()));

            var whiteListMatches = words.Where(w => whiteListItems.Contains(w.ToLower()));

            return new HashSet<string>(whiteListMatches);
        }
    }
}