namespace ProcessingTools.Bio.Taxonomy.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Contracts;
    using Models;

    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Types;

    public class SuffixHigherTaxaRankResolverDataService : ISuffixHigherTaxaRankResolverDataService
    {
        private IDictionary<string, TaxonRankType> rankPerSuffix;

        public SuffixHigherTaxaRankResolverDataService()
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

        public Task<IQueryable<ITaxonRank>> Resolve(params string[] scientificNames)
        {
            return Task.Run(() =>
            {
                var result = new HashSet<ITaxonRank>();

                foreach (var scientificName in scientificNames)
                {
                    var ranks = this.rankPerSuffix.Keys
                        .Where(s => Regex.IsMatch(scientificName, $"\\A[A-Z](?:(?i)[a-z]*{s})\\Z"))
                        .Select(k => this.rankPerSuffix[k])
                        .ToList();

                    ranks.ForEach(r => result.Add(new TaxonRankServiceModel
                    {
                        ScientificName = scientificName,
                        Rank = r
                    }));
                }

                return result.AsQueryable();
            });
        }
    }
}