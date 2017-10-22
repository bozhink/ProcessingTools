// <copyright file="IRegionsDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Data.Geo
{
    using ProcessingTools.Models.Contracts.Geo;

    /// <summary>
    /// Regions data service.
    /// </summary>
    public interface IRegionsDataService : IDataServiceAsync<IRegion, IRegionsFilter>, IGeoSynonymisableDataService<IRegion, IRegionSynonym, IRegionSynonymsFilter>
    {
    }
}
