// <copyright file="ITaxonRanksSearchService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Bio.Taxonomy
{
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    /// <summary>
    /// Taxon ranks search service.
    /// </summary>
    public interface ITaxonRanksSearchService : ISearchService<ITaxonRank, string>
    {
    }
}
