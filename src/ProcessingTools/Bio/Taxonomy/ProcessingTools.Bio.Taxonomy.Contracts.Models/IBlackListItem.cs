// <copyright file="IBlackListItem.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.Contracts.Models
{
    /// <summary>
    /// Black list item.
    /// </summary>
    public interface IBlackListItem
    {
        /// <summary>
        /// Gets content.
        /// </summary>
        string Content { get; }
    }
}
