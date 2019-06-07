﻿// <copyright file="ITaxonRankWithWhiteListing.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Bio.Taxonomy
{
    /// <summary>
    /// Taxon rank with white-listing.
    /// </summary>
    public interface ITaxonRankWithWhiteListing : ITaxonRank
    {
        /// <summary>
        /// Gets or sets a value indicating whether taxon is in the white list.
        /// </summary>
        bool IsWhiteListed { get; set; }
    }
}
