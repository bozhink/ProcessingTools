// <copyright file="TaxonRank.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.Models
{
    using ProcessingTools.Bio.Taxonomy.Common;
    using ProcessingTools.Bio.Taxonomy.Contracts.Models;

    /// <summary>
    /// Taxon rank service model.
    /// </summary>
    public class TaxonRank : ITaxonRank
    {
        /// <inheritdoc/>
        public TaxonRankType Rank { get; set; }

        /// <inheritdoc/>
        public string ScientificName { get; set; }
    }
}
