// <copyright file="IMunicipalitiesDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Models.Geo;

namespace ProcessingTools.Contracts.Services.Geo
{
    /// <summary>
    /// Municipalities data service.
    /// </summary>
    public interface IMunicipalitiesDataService : IDataServiceAsync<IMunicipality, IMunicipalitiesFilter>, IGeoSynonymisableDataService<IMunicipality, IMunicipalitySynonym, IMunicipalitySynonymsFilter>
    {
    }
}
