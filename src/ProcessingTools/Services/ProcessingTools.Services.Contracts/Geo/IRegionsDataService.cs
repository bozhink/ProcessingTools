// <copyright file="IRegionsDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Models.Geo;

namespace ProcessingTools.Contracts.Services.Geo
{
    /// <summary>
    /// Regions data service.
    /// </summary>
    public interface IRegionsDataService : IDataServiceAsync<IRegion, IRegionsFilter>, IGeoSynonymisableDataService<IRegion, IRegionSynonym, IRegionSynonymsFilter>
    {
    }
}
