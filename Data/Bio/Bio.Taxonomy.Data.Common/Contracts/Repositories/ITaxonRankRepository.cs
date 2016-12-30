﻿namespace ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Repositories
{
    using Models;
    using ProcessingTools.Contracts.Data.Repositories;

    public interface ITaxonRankRepository : IFiltrableRepository<ITaxonRankEntity>, IAddableRepository<ITaxonRankEntity>, IDeletableRepository<ITaxonRankEntity>, IUpdatableRepository<ITaxonRankEntity>, IQueryableRepository<ITaxonRankEntity>, ISavabaleRepository
    {
    }
}