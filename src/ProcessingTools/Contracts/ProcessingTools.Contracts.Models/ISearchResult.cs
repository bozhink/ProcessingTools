// <copyright file="ISearchResult.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models
{
    /// <summary>
    /// Search result.
    /// </summary>
    public interface ISearchResult
    {
        /// <summary>
        /// Gets or sets the search key.
        /// </summary>
        string SearchKey { get; set; }
    }
}
