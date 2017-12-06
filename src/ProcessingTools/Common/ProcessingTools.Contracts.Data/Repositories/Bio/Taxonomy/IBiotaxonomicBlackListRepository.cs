// <copyright file="IBiotaxonomicBlackListRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Repositories.Bio.Taxonomy
{
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;

    /// <summary>
    /// Biotaxonomic black-list repository.
    /// </summary>
    public interface IBiotaxonomicBlackListRepository : ICrudRepository<IBlackListEntity>, IIterableRepository<IBlackListEntity>, IRepository<IBlackListEntity>
    {
    }
}
