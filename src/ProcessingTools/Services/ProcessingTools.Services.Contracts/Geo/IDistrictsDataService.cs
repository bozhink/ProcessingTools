// <copyright file="IDistrictsDataService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Geo
{
    using ProcessingTools.Models.Contracts.Geo;

    /// <summary>
    /// Districts data service.
    /// </summary>
    public interface IDistrictsDataService : IDataServiceAsync<IDistrict, IDistrictsFilter>, IGeoSynonymisableDataService<IDistrict, IDistrictSynonym, IDistrictSynonymsFilter>
    {
    }
}
