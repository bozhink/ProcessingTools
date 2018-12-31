﻿// <copyright file="IBlackListItem.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Bio.Taxonomy
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
