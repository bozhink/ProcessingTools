// <copyright file="IGeoEpithetsDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Data.Geo
{
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Models.Contracts.Geo;

    public interface IGeoEpithetsDataService : IMultiDataServiceAsync<IGeoEpithet, ITextFilter>
    {
    }
}
