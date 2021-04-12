// <copyright file="AboveGenusTaxonRankResolver.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.Common;
    using ProcessingTools.Bio.Taxonomy.Contracts.Models;
    using ProcessingTools.Bio.Taxonomy.Models;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;

    /// <summary>
    /// Taxon rank resolver with above-genus rank.
    /// </summary>
    public class AboveGenusTaxonRankResolver : IAboveGenusTaxonRankResolver
    {
        /// <inheritdoc/>
        public async Task<IList<ITaxonRankSearchResult>> ResolveAsync(IEnumerable<string> names)
        {
            if (names is null || !names.Any())
            {
                return Array.Empty<ITaxonRankSearchResult>();
            }

            await Task.CompletedTask.ConfigureAwait(false);

            return names.Select(name => new TaxonRankSearchResult { ScientificName = name, Rank = TaxonRankType.AboveGenus }).ToArray<ITaxonRankSearchResult>();
        }
    }
}
