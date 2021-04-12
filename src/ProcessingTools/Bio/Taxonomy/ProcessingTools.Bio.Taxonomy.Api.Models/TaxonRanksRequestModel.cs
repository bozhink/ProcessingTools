// <copyright file="TaxonRanksRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.Api.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Taxon ranks request model.
    /// </summary>
    public class TaxonRanksRequestModel
    {
        /// <summary>
        /// Gets or sets the taxon-rank items.
        /// </summary>
        public IList<TaxonRankRequestModel> Items { get; set; } = new List<TaxonRankRequestModel>();
    }
}
