// <copyright file="TaxonRank.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Data.Bio.Taxonomy
{
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

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
