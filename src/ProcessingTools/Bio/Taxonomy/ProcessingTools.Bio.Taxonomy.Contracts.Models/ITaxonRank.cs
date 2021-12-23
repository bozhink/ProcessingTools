// <copyright file="ITaxonRank.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.Contracts.Models
{
    using ProcessingTools.Bio.Taxonomy.Common;

    /// <summary>
    /// Taxon rank model.
    /// </summary>
    public interface ITaxonRank
    {
        /// <summary>
        /// Gets the scientific name.
        /// </summary>
        string ScientificName { get; }

        /// <summary>
        /// Gets the taxon rank.
        /// </summary>
        TaxonRankType Rank { get; }
    }
}
