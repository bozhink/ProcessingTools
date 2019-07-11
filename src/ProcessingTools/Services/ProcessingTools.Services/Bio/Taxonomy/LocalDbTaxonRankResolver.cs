// <copyright file="LocalDbTaxonRankResolver.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.DataAccess.Bio.Taxonomy;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;
    using ProcessingTools.Services.Models.Data.Bio.Taxonomy;

    /// <summary>
    /// Taxon rank resolver with local DB.
    /// </summary>
    public class LocalDbTaxonRankResolver : ILocalDbTaxonRankResolver
    {
        private readonly ITaxonRanksDataAccessObject dataAccessObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalDbTaxonRankResolver"/> class.
        /// </summary>
        /// <param name="dataAccessObject">Data access object.</param>
        public LocalDbTaxonRankResolver(ITaxonRanksDataAccessObject dataAccessObject)
        {
            this.dataAccessObject = dataAccessObject ?? throw new ArgumentNullException(nameof(dataAccessObject));
        }

        /// <inheritdoc/>
        public async Task<IList<ITaxonRank>> ResolveAsync(IEnumerable<string> scientificNames)
        {
            var result = new ConcurrentQueue<ITaxonRank>();

            await this.ResolveAsync(scientificNames, result).ConfigureAwait(false);

            return result.ToArray();
        }

        private async Task ResolveAsync(IEnumerable<string> scientificNames, ConcurrentQueue<ITaxonRank> outputCollection)
        {
            if (scientificNames == null || !scientificNames.Any())
            {
                return;
            }

            Regex matchWhitespaces = new Regex(@"\s+", RegexOptions.Compiled);

            var names = scientificNames
                .Select(s => matchWhitespaces.Replace(s, string.Empty))
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => s.ToUpperInvariant())
                .Distinct()
                .ToList();

            var tasks = names.Select(name => this.FindRankForSingleTaxonAsync(name, outputCollection)).ToArray();

            await Task.WhenAll(tasks).ConfigureAwait(false);
        }

        private async Task FindRankForSingleTaxonAsync(string name, ConcurrentQueue<ITaxonRank> outputCollection)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            var items = await this.dataAccessObject.FindExactAsync(name).ConfigureAwait(false);

            if (items != null && items.Any())
            {
                foreach (var item in items)
                {
                    if (item.Ranks != null && item.Ranks.Any())
                    {
                        foreach (var rank in item.Ranks)
                        {
                            outputCollection.Enqueue(new TaxonRank
                            {
                                ScientificName = item.Name,
                                Rank = rank,
                            });
                        }
                    }
                }
            }
        }
    }
}
