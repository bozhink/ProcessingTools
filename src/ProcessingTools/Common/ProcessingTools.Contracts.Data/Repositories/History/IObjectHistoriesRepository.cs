// <copyright file="IObjectHistoriesRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Repositories.History
{
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Contracts.Models.History;

    /// <summary>
    /// Object histories repository.
    /// </summary>
    public interface IObjectHistoriesRepository : ICrudRepository<IObjectHistory>
    {
    }
}
