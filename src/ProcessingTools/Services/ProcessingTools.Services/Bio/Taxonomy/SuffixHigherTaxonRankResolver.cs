// <copyright file="SuffixHigherTaxonRankResolver.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Bio.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Models.Data.Bio.Taxonomy;

    /// <summary>
    /// Taxon rank resolver by suffix.
    /// </summary>
    public class SuffixHigherTaxonRankResolver : ISuffixHigherTaxonRankResolver
    {
        private readonly IDictionary<string, TaxonRankType> rankPerSuffix;

        /// <summary>
        /// Initializes a new instance of the <see cref="SuffixHigherTaxonRankResolver"/> class.
        /// </summary>
        public SuffixHigherTaxonRankResolver()
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

        /// <inheritdoc/>
        public Task<ITaxonRank[]> ResolveAsync(params string[] scientificNames)
        {
            return Task.Run(() =>
            {
                var result = new HashSet<ITaxonRank>();

                foreach (var scientificName in scientificNames)
                {
                    var ranks = this.rankPerSuffix.Keys
                        .Where(suffix => Regex.IsMatch(scientificName, $"\\A[A-Z](?:(?i)[a-z]*{suffix})\\Z"))
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

                return result.ToArray();
            });
        }
    }
}
