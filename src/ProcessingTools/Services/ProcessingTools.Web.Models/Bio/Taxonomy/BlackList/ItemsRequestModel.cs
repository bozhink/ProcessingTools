// <copyright file="ItemsRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Bio.Taxonomy.BlackList
{
    using System.Collections.Generic;

    /// <summary>
    /// Blacklist items request model.
    /// </summary>
    public class ItemsRequestModel
    {
        /// <summary>
        /// Gets or sets the blacklist items.
        /// </summary>
        public IList<ItemRequestModel> Items { get; set; } = new List<ItemRequestModel>();
    }
}
