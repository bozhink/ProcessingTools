// <copyright file="IRegionsDataService.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Geo
{
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Regions data service.
    /// </summary>
    public interface IRegionsDataService : IDataServiceAsync<IRegion, IRegionsFilter>, IGeoSynonymisableDataService<IRegion, IRegionSynonym, IRegionSynonymsFilter>
    {
    }
}
