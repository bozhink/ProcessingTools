// <copyright file="IDistrictsDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Data.Geo
{
    using ProcessingTools.Contracts.Filters.Geo;
    using ProcessingTools.Models.Contracts.Geo;

    /// <summary>
    /// Districts data service.
    /// </summary>
    public interface IDistrictsDataService : IDataServiceAsync<IDistrict, IDistrictsFilter>, IGeoSynonymisableDataService<IDistrict, IDistrictSynonym, IDistrictSynonymsFilter>
    {
    }
}
