// <copyright file="IHistoryRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.History.Repositories
{
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Models.Contracts.History;

    /// <summary>
    /// History repository.
    /// </summary>
    public interface IHistoryRepository : ICrudRepository<IHistoryItem>
    {
    }
}
