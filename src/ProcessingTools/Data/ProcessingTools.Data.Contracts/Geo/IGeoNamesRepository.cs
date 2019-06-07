// <copyright file="IGeoNamesRepository.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Geo
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Geo names repository.
    /// </summary>
    public interface IGeoNamesRepository : IRepositoryAsync<IGeoName, ITextFilter>
    {
    }
}
