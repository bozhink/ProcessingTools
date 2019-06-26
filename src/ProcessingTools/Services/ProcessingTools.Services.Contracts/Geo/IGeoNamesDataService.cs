// <copyright file="IGeoNamesDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Geo
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Geo names data service.
    /// </summary>
    public interface IGeoNamesDataService : ISelectableDataServiceAsync<IGeoName, ITextFilter>
    {
    }
}
