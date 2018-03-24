// <copyright file="IGeoEpithetsDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Geo
{
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Geo;

    /// <summary>
    /// Geo epithets data service.
    /// </summary>
    public interface IGeoEpithetsDataService : IMultiDataServiceAsync<IGeoEpithet, ITextFilter>
    {
    }
}
