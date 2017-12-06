// <copyright file="IContinentsRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Repositories.Geo
{
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Continents repository.
    /// </summary>
    public interface IContinentsRepository : IRepositoryAsync<IContinent, IContinentsFilter>, IGeoSynonymisableRepository<IContinent, IContinentSynonym, IContinentSynonymsFilter>
    {
    }
}
