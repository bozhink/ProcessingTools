// <copyright file="ITaxonRanksRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Bio.Taxonomy.Repositories
{
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    /// <summary>
    /// Taxon ranks repository.
    /// </summary>
    public interface ITaxonRanksRepository : ICrudRepository<ITaxonRankEntity>
    {
    }
}
