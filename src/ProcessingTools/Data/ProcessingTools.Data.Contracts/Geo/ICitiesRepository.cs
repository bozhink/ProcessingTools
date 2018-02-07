// <copyright file="ICitiesRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Geo
{
    using ProcessingTools.Models.Contracts.Geo;

    /// <summary>
    /// Cities repository.
    /// </summary>
    public interface ICitiesRepository : IRepositoryAsync<ICity, ICitiesFilter>, IGeoSynonymisableRepository<ICity, ICitySynonym, ICitySynonymsFilter>
    {
    }
}
