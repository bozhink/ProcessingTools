// <copyright file="TaxonRank.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.Aphia
{
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;

    /// <summary>
    /// Taxon rank.
    /// </summary>
    public class TaxonRank : ITaxonRank
    {
        /// <inheritdoc/>
        public string ScientificName { get; set; }

        /// <inheritdoc/>
        public TaxonRankType Rank { get; set; }
    }
}
