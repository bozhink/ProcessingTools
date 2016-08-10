namespace ProcessingTools.Bio.Taxonomy.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Contracts;
    using Models;

    using ProcessingTools.Bio.Taxonomy.Contracts;

    public class SuffixHigherTaxaRankResolverDataService : ISuffixHigherTaxaRankResolverDataService
    {
        private IDictionary<string, string> rankPerSuffix;

        public SuffixHigherTaxaRankResolverDataService()
        {
            this.rankPerSuffix = new Dictionary<string, string>()
            {
                { "phyta|mycota", "phylum" },
                { "phytina|mycotina", "subphylum" },
                { "ia|opsida|phyceae|mycetes", "class" },
                { "idae|phycidae|mycetidae",  "subclass" },
                { "anae", "superorder" },
                { "ales", "order" },
                { "ineae", "suborder" },
                { "aria", "infraorder" },
                { "acea|oidea", "superfamily" },
                { "oidae", "epifamily" },
                { "aceae|idae", "family" },
                { "oideae|inae", "subfamily" },
                { "eae|ini", "tribe" },
                { "inae|ina", "subtribe" }
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