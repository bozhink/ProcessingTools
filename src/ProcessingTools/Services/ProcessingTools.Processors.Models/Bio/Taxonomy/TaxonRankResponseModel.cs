// <copyright file="TaxonRankResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Bio.Taxonomy
{
    /// <summary>
    /// Taxon rank response model.
    /// </summary>
    public class TaxonRankResponseModel
    {
        /// <summary>
        /// Gets or sets the scientific name.
        /// </summary>
        public string ScientificName { get; set; }

        /// <summary>
        /// Gets or sets the rank.
        /// </summary>
        public string Rank { get; set; }
    }
}
