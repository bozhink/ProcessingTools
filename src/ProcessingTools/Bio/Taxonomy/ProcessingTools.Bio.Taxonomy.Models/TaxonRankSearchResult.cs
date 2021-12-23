// <copyright file="TaxonRankSearchResult.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.Models
{
    using ProcessingTools.Bio.Taxonomy.Contracts.Models;

    /// <summary>
    /// Taxon rank search result.
    /// </summary>
    public class TaxonRankSearchResult : TaxonRank, ITaxonRankSearchResult
    {
        /// <inheritdoc/>
        public string SearchKey { get; set; }
    }
}
