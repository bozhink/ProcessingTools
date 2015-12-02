namespace ProcessingTools.Bio.Taxonomy.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Contracts;
    using Models;
    using Taxonomy.Contracts;

    public class SuffixHigherTaxaRankDataService : ISuffixHigherTaxaRankDataService
    {
        private IDictionary<string, string> rankPerSuffix;

        public SuffixHigherTaxaRankDataService()
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

        public IQueryable<ITaxonRank> Resolve(params string[] scientificNames)
        {
            var result = new HashSet<ITaxonRank>();

            foreach (var scientificName in scientificNames)
            {
                var ranks = this.rankPerSuffix.Keys
                    .Where(s => Regex.IsMatch(scientificName, $"\\A[A-Z](?:(?i)[a-z]*{s})\\Z"))
                    .Select(k => this.rankPerSuffix[k])
                    .ToList();

                ranks.ForEach(r => result.Add(new TaxonRankDataServiceResponseModel
                {
                    ScientificName = scientificName,
                    Rank = r
                }));
            }

            return result.AsQueryable();
        }
    }
}