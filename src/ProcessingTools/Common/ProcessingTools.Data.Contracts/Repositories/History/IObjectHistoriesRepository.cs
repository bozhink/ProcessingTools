// <copyright file="IObjectHistoriesRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Repositories.History
{
    using ProcessingTools.Data.Contracts.Repositories;
    using ProcessingTools.Models.Contracts.History;

    /// <summary>
    /// Object histories repository.
    /// </summary>
    public interface IObjectHistoriesRepository : ICrudRepository<IObjectHistory>
    {
    }
}
