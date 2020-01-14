// <copyright file="ITaxonRankResolver.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Bio.Taxonomy
{
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;

    /// <summary>
    /// Taxon rank resolver.
    /// </summary>
    public interface ITaxonRankResolver : ITaxonInformationResolver<ITaxonRankSearchResult>
    {
    }
}
