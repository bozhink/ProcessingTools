// <copyright file="BlackListSearchResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.Api.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Blacklist search response model.
    /// </summary>
    public class BlackListSearchResponseModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BlackListSearchResponseModel"/> class.
        /// </summary>
        /// <param name="items">Collection of blacklist items.</param>
        public BlackListSearchResponseModel(IEnumerable<BlackListItemResponseModel> items)
        {
            this.Items = items ?? Array.Empty<BlackListItemResponseModel>();
        }

        /// <summary>
        /// Gets the blacklist items.
        /// </summary>
        public IEnumerable<BlackListItemResponseModel> Items { get; }
    }
}
