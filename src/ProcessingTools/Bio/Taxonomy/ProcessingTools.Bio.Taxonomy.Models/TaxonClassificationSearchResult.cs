// <copyright file="TaxonClassificationSearchResult.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.Models
{
    using ProcessingTools.Bio.Taxonomy.Contracts.Models;

    /// <summary>
    /// Taxon classification search result.
    /// </summary>
    public class TaxonClassificationSearchResult : TaxonClassification, ITaxonClassificationSearchResult
    {
        /// <inheritdoc/>
        public string SearchKey { get; set; }
    }
}
