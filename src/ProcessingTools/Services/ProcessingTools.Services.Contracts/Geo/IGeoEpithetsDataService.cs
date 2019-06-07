// <copyright file="IGeoEpithetsDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Geo
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Geo epithets data service.
    /// </summary>
    public interface IGeoEpithetsDataService : IMultiDataServiceAsync<IGeoEpithet, ITextFilter>
    {
    }
}
