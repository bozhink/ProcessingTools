// <copyright file="ITaxonRanksRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Repositories.Bio.Taxonomy
{
    using ProcessingTools.Data.Contracts.Repositories;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    /// <summary>
    /// Taxon ranks repository.
    /// </summary>
    public interface ITaxonRanksRepository : ICrudRepository<ITaxonRankEntity>
    {
    }
}
