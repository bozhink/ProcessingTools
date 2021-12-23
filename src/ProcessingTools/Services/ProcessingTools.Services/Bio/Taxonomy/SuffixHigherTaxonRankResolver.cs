// <copyright file="SuffixHigherTaxonRankResolver.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Bio.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.Common;
    using ProcessingTools.Bio.Taxonomy.Contracts.Models;
    using ProcessingTools.Bio.Taxonomy.Models;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;

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
                { "inae|ina", TaxonRankType.Subtribe },
            };
        }

        /// <inheritdoc/>
        public Task<IList<ITaxonRankSearchResult>> ResolveAsync(IEnumerable<string> names)
        {
            return Task.Run<IList<ITaxonRankSearchResult>>(() =>
            {
                var result = new HashSet<ITaxonRankSearchResult>();

                foreach (var name in names)
                {
                    var ranks = this.rankPerSuffix.Keys
                        .Where(suffix => Regex.IsMatch(name, $"\\A[A-Z](?:(?i)[a-z]*{suffix})\\Z"))
                        .Select(k => this.rankPerSuffix[k]);

                    foreach (var rank in ranks)
                    {
                        result.Add(new TaxonRankSearchResult
                        {
                            ScientificName = name,
                            Rank = rank,
                        });
                    }
                }

                return result.ToArray();
            });
        }
    }
}
