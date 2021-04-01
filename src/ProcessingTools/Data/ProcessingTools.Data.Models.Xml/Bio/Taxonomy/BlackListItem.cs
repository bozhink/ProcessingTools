// <copyright file="BlackListItem.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Xml.Bio.Taxonomy
{
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;

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
