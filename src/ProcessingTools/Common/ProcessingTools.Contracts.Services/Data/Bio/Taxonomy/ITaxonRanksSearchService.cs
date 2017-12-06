// <copyright file="ITaxonRanksSearchService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Data.Bio.Taxonomy
{
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;

    /// <summary>
    /// Taxon ranks search service.
    /// </summary>
    public interface ITaxonRanksSearchService : ISearchService<ITaxonRank, string>
    {
    }
}
