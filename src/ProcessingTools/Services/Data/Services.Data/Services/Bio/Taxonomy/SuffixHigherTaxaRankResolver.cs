namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Models.Data.Bio.Taxonomy;

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

        public Task<IEnumerable<ITaxonRank>> Resolve(params string[] scientificNames)
        {
            var result = new HashSet<ITaxonRank>();

            foreach (var scientificName in scientificNames)
            {
                var ranks = this.rankPerSuffix.Keys
                    .Where(s => Regex.IsMatch(scientificName, $"\\A[A-Z](?:(?i)[a-z]*{s})\\Z"))
                    .Select(k => this.rankPerSuffix[k]);

                foreach (var rank in ranks)
                {
                    result.Add(new TaxonRank
                    {
                        ScientificName = scientificName,
                        Rank = rank
                    });
                }
            }

            return Task.FromResult<IEnumerable<ITaxonRank>>(result);
        }
    }
}
