// <copyright file="ITaxonSearchResult.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.Contracts.Models
{
    /// <summary>
    /// Taxon search result.
    /// </summary>
    public interface ITaxonSearchResult
    {
        /// <summary>
        /// Gets or sets the search key.
        /// </summary>
        string? SearchKey { get; set; }
    }
}
