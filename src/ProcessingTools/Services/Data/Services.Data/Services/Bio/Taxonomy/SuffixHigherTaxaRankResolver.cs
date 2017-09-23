namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Contracts.Bio.Taxonomy;
    using Models.Bio.Taxonomy;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Enumerations;

    public class SuffixHigherTaxaRankResolver : ISuffixHigherTaxaRankResolver
    {
        private readonly IDictionary<string, TaxonRankType> rankPerSuffix;

        public SuffixHigherTaxaRankResolver()
        {
            this.rankPerSuffix = new Dictionary<string, TaxonRankType>()
            {
                { "phyta|mycota", TaxonRankType.Phylum },
                { "phytina|mycotina", TaxonRankType.Subphylum },
                { "ia|opsida|phyceae|mycetes", TaxonRankType.Class },
                { "idae|phycidae|mycetidae", TaxonRankType.Subclass },
                { "anae", TaxonRankType.Superorder },
                { "ales", TaxonRankType.Order },
                { "ineae", TaxonRankType.Suborder },
                { "aria", TaxonRankType.Infraorder },
                { "acea|oidea", TaxonRankType.Superfamily },
                { "oidae", TaxonRankType.Epifamily },
                { "aceae|idae", TaxonRankType.Family },
                { "oideae|inae", TaxonRankType.Subfamily },
                { "eae|ini", TaxonRankType.Tribe },
                { "inae|ina", TaxonRankType.Subtribe }
            };
        }

        public async Task<IEnumerable<ITaxonRank>> Resolve(params string[] scientificNames)
        {
            var result = new HashSet<ITaxonRank>();

            foreach (var scientificName in scientificNames)
            {
                var ranks = this.rankPerSuffix.Keys
                    .Where(s => Regex.IsMatch(scientificName, $"\\A[A-Z](?:(?i)[a-z]*{s})\\Z"))
                    .Select(k => this.rankPerSuffix[k]);

                foreach (var rank in ranks)
                {
                    result.Add(new TaxonRankServiceModel
                    {
                        ScientificName = scientificName,
                        Rank = rank
                    });
                }
            }

            return await Task.FromResult(result).ConfigureAwait(false);
        }
    }
}
