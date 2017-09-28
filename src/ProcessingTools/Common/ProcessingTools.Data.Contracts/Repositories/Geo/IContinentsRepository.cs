// <copyright file="IContinentsRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Repositories.Geo
{
    using ProcessingTools.Models.Contracts.Geo;

    /// <summary>
    /// Continents repository.
    /// </summary>
    public interface IContinentsRepository : IRepositoryAsync<IContinent, IContinentsFilter>, IGeoSynonymisableRepository<IContinent, IContinentSynonym, IContinentSynonymsFilter>
    {
    }
}
