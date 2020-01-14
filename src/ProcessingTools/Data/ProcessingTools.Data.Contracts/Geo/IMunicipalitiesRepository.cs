// <copyright file="IMunicipalitiesRepository.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Geo
{
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Municipalities repository.
    /// </summary>
    public interface IMunicipalitiesRepository : IRepositoryAsync<IMunicipality, IMunicipalitiesFilter>, IGeoSynonymisableRepository<IMunicipality, IMunicipalitySynonym, IMunicipalitySynonymsFilter>
    {
    }
}
