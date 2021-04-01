// <copyright file="ICitiesDataService.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Geo
{
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Cities data service.
    /// </summary>
    public interface ICitiesDataService : IDataServiceAsync<ICity, ICitiesFilter>, IGeoSynonymisableDataService<ICity, ICitySynonym, ICitySynonymsFilter>
    {
    }
}
