// <copyright file="TaxonClassificationSearchResult.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Data.Bio.Taxonomy
{
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;

    /// <summary>
    /// Taxon classification search result.
    /// </summary>
    public class TaxonClassificationSearchResult : TaxonClassification, ITaxonClassificationSearchResult
    {
        /// <inheritdoc/>
        public string SearchKey { get; set; }
    }
}
