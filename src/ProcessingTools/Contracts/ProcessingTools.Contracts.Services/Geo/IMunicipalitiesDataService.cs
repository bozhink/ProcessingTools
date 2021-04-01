// <copyright file="IMunicipalitiesDataService.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Geo
{
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Municipalities data service.
    /// </summary>
    public interface IMunicipalitiesDataService : IDataServiceAsync<IMunicipality, IMunicipalitiesFilter>, IGeoSynonymisableDataService<IMunicipality, IMunicipalitySynonym, IMunicipalitySynonymsFilter>
    {
    }
}
