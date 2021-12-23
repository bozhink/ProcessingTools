// <copyright file="IGeoEpithetsDataService.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Geo
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Geo epithets data service.
    /// </summary>
    public interface IGeoEpithetsDataService : ISelectableDataServiceAsync<IGeoEpithet, ITextFilter>
    {
    }
}
