// <copyright file="IObjectHistoryRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.History
{
    using ProcessingTools.Models.Contracts.History;

    /// <summary>
    /// Object histories repository.
    /// </summary>
    public interface IObjectHistoryRepository : ICrudRepository<IObjectHistory>
    {
    }
}
