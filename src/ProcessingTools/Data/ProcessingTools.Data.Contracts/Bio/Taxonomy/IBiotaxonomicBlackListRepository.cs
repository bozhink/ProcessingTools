// <copyright file="IBiotaxonomicBlackListRepository.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Bio.Taxonomy
{
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    /// <summary>
    /// Biotaxonomic black-list repository.
    /// </summary>
    public interface IBiotaxonomicBlackListRepository : ICrudRepository<IBlackListItem>, IIterableRepository<IBlackListItem>
    {
    }
}
