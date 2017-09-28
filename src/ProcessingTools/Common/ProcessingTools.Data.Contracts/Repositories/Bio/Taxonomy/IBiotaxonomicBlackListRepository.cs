// <copyright file="IBiotaxonomicBlackListRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Repositories.Bio.Taxonomy
{
    using ProcessingTools.Data.Contracts.Repositories;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    /// <summary>
    /// Biotaxonomic black-list repository.
    /// </summary>
    public interface IBiotaxonomicBlackListRepository : ICrudRepository<IBlackListEntity>, IIterableRepository<IBlackListEntity>, IRepository<IBlackListEntity>
    {
    }
}
