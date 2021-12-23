// <copyright file="BlackListItemsRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.Api.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Blacklist items request model.
    /// </summary>
    public class BlackListItemsRequestModel
    {
        /// <summary>
        /// Gets or sets the blacklist items.
        /// </summary>
        public IList<BlackListItemRequestModel> Items { get; set; } = new List<BlackListItemRequestModel>();
    }
}
