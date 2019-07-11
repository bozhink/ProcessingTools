// <copyright file="IDistrictsDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Geo
{
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Districts data service.
    /// </summary>
    public interface IDistrictsDataService : IDataServiceAsync<IDistrict, IDistrictsFilter>, IGeoSynonymisableDataService<IDistrict, IDistrictSynonym, IDistrictSynonymsFilter>
    {
    }
}
