// <copyright file="SearchResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Bio.Taxonomy.TaxonRanks
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Taxon ranks search response model.
    /// </summary>
    public class SearchResponseModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchResponseModel"/> class.
        /// </summary>
        /// <param name="items">Collection of taxon rank items.</param>
        public SearchResponseModel(IEnumerable<TaxonRankResponseModel> items)
        {
            this.Items = items ?? Array.Empty<TaxonRankResponseModel>();
        }

        /// <summary>
        /// Gets the taxon rank items.
        /// </summary>
        public IEnumerable<TaxonRankResponseModel> Items { get; }
    }
}
