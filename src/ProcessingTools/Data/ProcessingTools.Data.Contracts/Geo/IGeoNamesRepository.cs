// <copyright file="IGeoNamesRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Geo
{
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Geo;

    /// <summary>
    /// Geo names repository.
    /// </summary>
    public interface IGeoNamesRepository : IRepositoryAsync<IGeoName, ITextFilter>
    {
    }
}
