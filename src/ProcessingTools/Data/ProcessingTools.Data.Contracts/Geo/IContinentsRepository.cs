// <copyright file="IContinentsRepository.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Geo
{
    using ProcessingTools.Models.Contracts.Geo;

    /// <summary>
    /// Continents repository.
    /// </summary>
    public interface IContinentsRepository : IRepositoryAsync<IContinent, IContinentsFilter>, IGeoSynonymisableRepository<IContinent, IContinentSynonym, IContinentSynonymsFilter>
    {
    }
}
