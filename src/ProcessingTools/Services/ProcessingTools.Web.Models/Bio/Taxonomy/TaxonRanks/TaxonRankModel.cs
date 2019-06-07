// <copyright file="TaxonRankModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Bio.Taxonomy.TaxonRanks
{
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;

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
