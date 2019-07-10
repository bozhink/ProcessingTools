// <copyright file="IGeoEpithetsDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Models;
using ProcessingTools.Contracts.Models.Geo;

namespace ProcessingTools.Contracts.Services.Geo
{
    /// <summary>
    /// Geo epithets data service.
    /// </summary>
    public interface IGeoEpithetsDataService : ISelectableDataServiceAsync<IGeoEpithet, ITextFilter>
    {
    }
}
