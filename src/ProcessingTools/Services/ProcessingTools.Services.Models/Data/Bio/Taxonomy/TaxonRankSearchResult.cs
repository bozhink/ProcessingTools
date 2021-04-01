// <copyright file="TaxonRankSearchResult.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Data.Bio.Taxonomy
{
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;

    /// <summary>
    /// Taxon rank search result.
    /// </summary>
    public class TaxonRankSearchResult : TaxonRank, ITaxonRankSearchResult
    {
        /// <inheritdoc/>
        public string SearchKey { get; set; }
    }
}
