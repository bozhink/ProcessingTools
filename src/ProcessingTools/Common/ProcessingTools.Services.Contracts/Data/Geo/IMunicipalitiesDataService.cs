// <copyright file="IMunicipalitiesDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Data.Geo
{
    using ProcessingTools.Models.Contracts.Geo;

    /// <summary>
    /// Municipalities data service.
    /// </summary>
    public interface IMunicipalitiesDataService : IDataServiceAsync<IMunicipality, IMunicipalitiesFilter>, IGeoSynonymisableDataService<IMunicipality, IMunicipalitySynonym, IMunicipalitySynonymsFilter>
    {
    }
}
