// <copyright file="IMunicipalitiesDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Geo
{
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Municipalities data service.
    /// </summary>
    public interface IMunicipalitiesDataService : IDataServiceAsync<IMunicipality, IMunicipalitiesFilter>, IGeoSynonymisableDataService<IMunicipality, IMunicipalitySynonym, IMunicipalitySynonymsFilter>
    {
    }
}
