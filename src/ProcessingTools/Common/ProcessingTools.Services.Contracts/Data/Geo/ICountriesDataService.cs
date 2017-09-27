// <copyright file="ICountriesDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Data.Geo
{
    using ProcessingTools.Contracts.Filters.Geo;
    using ProcessingTools.Models.Contracts.Geo;

    /// <summary>
    /// Countries data service.
    /// </summary>
    public interface ICountriesDataService : IDataServiceAsync<ICountry, ICountriesFilter>, IGeoSynonymisableDataService<ICountry, ICountrySynonym, ICountrySynonymsFilter>
    {
    }
}
