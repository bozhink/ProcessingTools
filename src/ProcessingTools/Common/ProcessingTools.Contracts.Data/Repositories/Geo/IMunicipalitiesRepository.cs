// <copyright file="IMunicipalitiesRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Repositories.Geo
{
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Municipalities repository.
    /// </summary>
    public interface IMunicipalitiesRepository : IRepositoryAsync<IMunicipality, IMunicipalitiesFilter>, IGeoSynonymisableRepository<IMunicipality, IMunicipalitySynonym, IMunicipalitySynonymsFilter>
    {
    }
}
