// <copyright file="ITaxonRanksRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Repositories.Bio.Taxonomy
{
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;

    /// <summary>
    /// Taxon ranks repository.
    /// </summary>
    public interface ITaxonRanksRepository : ICrudRepository<ITaxonRankEntity>
    {
    }
}
