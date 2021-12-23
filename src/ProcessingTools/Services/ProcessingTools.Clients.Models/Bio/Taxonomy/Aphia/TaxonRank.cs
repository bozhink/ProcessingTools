// <copyright file="TaxonRank.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.Aphia
{
    using ProcessingTools.Bio.Taxonomy.Common;
    using ProcessingTools.Bio.Taxonomy.Contracts.Models;

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
