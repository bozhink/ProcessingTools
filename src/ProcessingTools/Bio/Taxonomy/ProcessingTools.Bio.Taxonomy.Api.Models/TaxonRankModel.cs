// <copyright file="TaxonRankModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.Api.Models
{
    using ProcessingTools.Bio.Taxonomy.Common;
    using ProcessingTools.Bio.Taxonomy.Contracts.Models;

    /// <summary>
    /// Taxon-rank model.
    /// </summary>
    public class TaxonRankModel : ITaxonRank
    {
        /// <summary>
        /// Gets or sets the taxon name.
        /// </summary>
        public string ScientificName { get; set; }

        /// <summary>
        /// Gets or sets the taxon rank.
        /// </summary>
        public TaxonRankType Rank { get; set; }
    }
}
