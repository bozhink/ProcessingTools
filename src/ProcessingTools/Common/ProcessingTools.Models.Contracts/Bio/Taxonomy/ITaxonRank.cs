// <copyright file="ITaxonRank.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Bio.Taxonomy
{
    using ProcessingTools.Enumerations;

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
