// <copyright file="IMunicipalitiesRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Geo
{
    using ProcessingTools.Models.Contracts.Geo;

    /// <summary>
    /// Municipalities repository.
    /// </summary>
    public interface IMunicipalitiesRepository : IRepositoryAsync<IMunicipality, IMunicipalitiesFilter>, IGeoSynonymisableRepository<IMunicipality, IMunicipalitySynonym, IMunicipalitySynonymsFilter>
    {
    }
}
