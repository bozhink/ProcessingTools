// <copyright file="ITaxonClassificationSearchResult.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Bio.Taxonomy
{
    /// <summary>
    /// Taxon classification search result.
    /// </summary>
    public interface ITaxonClassificationSearchResult : ITaxonClassification, ITaxonRankSearchResult
    {
    }
}
