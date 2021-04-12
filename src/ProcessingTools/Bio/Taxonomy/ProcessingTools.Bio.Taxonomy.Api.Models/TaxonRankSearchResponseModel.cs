// <copyright file="TaxonRankSearchResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.Api.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Taxon ranks search response model.
    /// </summary>
    public class TaxonRankSearchResponseModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonRankSearchResponseModel"/> class.
        /// </summary>
        /// <param name="items">Collection of taxon rank items.</param>
        public TaxonRankSearchResponseModel(IEnumerable<TaxonRankResponseModel> items)
        {
            this.Items = items ?? Array.Empty<TaxonRankResponseModel>();
        }

        /// <summary>
        /// Gets the taxon rank items.
        /// </summary>
        public IEnumerable<TaxonRankResponseModel> Items { get; }
    }
}
