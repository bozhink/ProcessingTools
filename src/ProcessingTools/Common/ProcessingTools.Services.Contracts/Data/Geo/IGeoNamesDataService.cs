// <copyright file="IGeoNamesDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Data.Geo
{
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Geo;

    /// <summary>
    /// Geo names data service.
    /// </summary>
    public interface IGeoNamesDataService : IMultiDataServiceAsync<IGeoName, ITextFilter>
    {
    }
}
