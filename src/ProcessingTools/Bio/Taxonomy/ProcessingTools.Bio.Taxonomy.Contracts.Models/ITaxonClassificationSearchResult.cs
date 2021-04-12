// <copyright file="ITaxonClassificationSearchResult.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.Contracts.Models
{
    /// <summary>
    /// Taxon classification search result.
    /// </summary>
    public interface ITaxonClassificationSearchResult : ITaxonClassification, ITaxonRankSearchResult
    {
    }
}
