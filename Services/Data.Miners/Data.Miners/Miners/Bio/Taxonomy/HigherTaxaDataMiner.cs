namespace ProcessingTools.Data.Miners.Miners.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.Constants;
    using ProcessingTools.Data.Miners.Contracts.Miners.Bio.Taxonomy;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;
    using ProcessingTools.Strings.Extensions;

    public class HigherTaxaDataMiner : IHigherTaxaDataMiner
    {
        private readonly Regex matchHigherTaxa = new Regex(TaxaRegexPatterns.HigherTaxaMatchPattern);

        private readonly IWhiteList whitelist;

        public HigherTaxaDataMiner(IWhiteList whitelist)
        {
            if (whitelist == null)
            {
                throw new ArgumentNullException(nameof(whitelist));
            }

            this.whitelist = whitelist;
        }

        public async Task<IEnumerable<string>> Mine(string content)
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
            return result;
        }

        private async Task<IEnumerable<string>> MatchWithWhiteList(IEnumerable<string> words)
        {
            var whiteListItems = await this.whitelist.Items;
            var seed = new HashSet<string>(whiteListItems.Select(t => t.ToLower()));

            var matches = words.Where(w => seed.Contains(w.ToLower()));

            return new HashSet<string>(matches);
        }
    }
}
