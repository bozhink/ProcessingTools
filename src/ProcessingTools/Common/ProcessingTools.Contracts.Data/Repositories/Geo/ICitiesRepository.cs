// <copyright file="ICitiesRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Repositories.Geo
{
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Cities repository.
    /// </summary>
    public interface ICitiesRepository : IRepositoryAsync<ICity, ICitiesFilter>, IGeoSynonymisableRepository<ICity, ICitySynonym, ICitySynonymsFilter>
    {
    }
}
