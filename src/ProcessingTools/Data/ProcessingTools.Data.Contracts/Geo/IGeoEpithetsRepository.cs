// <copyright file="IGeoEpithetsRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Geo
{
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Geo;

    /// <summary>
    /// Geo epithets repository.
    /// </summary>
    public interface IGeoEpithetsRepository : IRepositoryAsync<IGeoEpithet, ITextFilter>
    {
    }
}
