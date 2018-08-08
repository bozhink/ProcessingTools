// <copyright file="AboveGenusTaxonRankResolver.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Bio.Taxonomy
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Models.Data.Bio.Taxonomy;

    /// <summary>
    /// Taxon rank resolver with above-genus rank.
    /// </summary>
    public class AboveGenusTaxonRankResolver : IAboveGenusTaxonRankResolver
    {
        /// <inheritdoc/>
        public Task<ITaxonRank[]> ResolveAsync(params string[] scientificNames)
        {
            if (scientificNames == null || !scientificNames.Any())
            {
                return Task.FromResult(Array.Empty<ITaxonRank>());
            }

            return Task.Run(() => scientificNames
                .Select(scientificName => new TaxonRank
                {
                    ScientificName = scientificName,
                    Rank = TaxonRankType.AboveGenus
                })
                .ToArray<ITaxonRank>());
        }
    }
}
