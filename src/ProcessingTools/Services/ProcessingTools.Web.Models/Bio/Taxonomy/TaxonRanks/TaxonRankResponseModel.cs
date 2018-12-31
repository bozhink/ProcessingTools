// <copyright file="TaxonRankResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Bio.Taxonomy.TaxonRanks
{
    /// <summary>
    /// Taxon rank response model.
    /// </summary>
    public class TaxonRankResponseModel
    {
        /// <summary>
        /// Gets or sets the taxon name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the taxon rank.
        /// </summary>
        public string Rank { get; set; }
    }
}
