// <copyright file="ITaxonRankDataService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Bio.Taxonomy
{
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    /// <summary>
    /// Taxon rank data service.
    /// </summary>
    public interface ITaxonRankDataService : IAddableDataService<ITaxonRank>, IDeletableDataService<ITaxonRank>
    {
    }
}
