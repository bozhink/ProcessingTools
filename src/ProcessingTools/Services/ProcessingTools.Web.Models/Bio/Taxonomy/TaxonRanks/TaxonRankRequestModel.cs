// <copyright file="TaxonRankRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Bio.Taxonomy.TaxonRanks
{
    /// <summary>
    /// Taxon rank request model.
    /// </summary>
    public class TaxonRankRequestModel
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
