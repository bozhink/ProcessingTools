// <copyright file="SearchResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Bio.Taxonomy.BlackList
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Blacklist search response model.
    /// </summary>
    public class SearchResponseModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchResponseModel"/> class.
        /// </summary>
        /// <param name="items">Collection of blacklist items.</param>
        public SearchResponseModel(IEnumerable<ItemResponseModel> items)
        {
            this.Items = items ?? Array.Empty<ItemResponseModel>();
        }

        /// <summary>
        /// Gets the blacklist items.
        /// </summary>
        public IEnumerable<ItemResponseModel> Items { get; }
    }
}
