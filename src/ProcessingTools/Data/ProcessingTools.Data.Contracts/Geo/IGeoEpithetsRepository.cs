// <copyright file="IGeoEpithetsRepository.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Geo
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Geo epithets repository.
    /// </summary>
    public interface IGeoEpithetsRepository : IRepositoryAsync<IGeoEpithet, ITextFilter>
    {
    }
}
