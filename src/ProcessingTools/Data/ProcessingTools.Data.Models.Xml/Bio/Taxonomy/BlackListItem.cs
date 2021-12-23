// <copyright file="BlackListItem.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Xml.Bio.Taxonomy
{
    using ProcessingTools.Bio.Taxonomy.Contracts.Models;

    /// <summary>
    /// Black list item.
    /// </summary>
    public class BlackListItem : IBlackListItem
    {
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        public string Content { get; set; }
    }
}
