// <copyright file="AboveGenusTaxonRankResolver.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Models.Data.Bio.Taxonomy;

    /// <summary>
    /// Taxon rank resolver with above-genus rank.
    /// </summary>
    public class AboveGenusTaxonRankResolver : IAboveGenusTaxonRankResolver
    {
        /// <inheritdoc/>
        public Task<IList<ITaxonRank>> ResolveAsync(IEnumerable<string> scientificNames)
        {
            if (scientificNames == null || !scientificNames.Any())
            {
                return Task.FromResult<IList<ITaxonRank>>(Array.Empty<ITaxonRank>());
            }

            return Task.Run<IList<ITaxonRank>>(() => scientificNames
                .Select(scientificName => new TaxonRank
                {
                    ScientificName = scientificName,
                    Rank = TaxonRankType.AboveGenus,
                })
                .ToArray<ITaxonRank>());
        }
    }
}
