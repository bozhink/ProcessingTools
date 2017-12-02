// <copyright file="ITaxonRanksSearchService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Data.Bio.Taxonomy
{
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    /// <summary>
    /// Taxon ranks search service.
    /// </summary>
    public interface ITaxonRanksSearchService : ISearchService<ITaxonRank, string>
    {
    }
}
