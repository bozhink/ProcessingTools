// <copyright file="ICountriesDataService.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Geo
{
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Countries data service.
    /// </summary>
    public interface ICountriesDataService : IDataServiceAsync<ICountry, ICountriesFilter>, IGeoSynonymisableDataService<ICountry, ICountrySynonym, ICountrySynonymsFilter>
    {
    }
}
