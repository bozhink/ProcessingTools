// <copyright file="ICountriesRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Repositories.Geo
{
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Countries repository.
    /// </summary>
    public interface ICountriesRepository : IRepositoryAsync<ICountry, ICountriesFilter>, IGeoSynonymisableRepository<ICountry, ICountrySynonym, ICountrySynonymsFilter>
    {
    }
}
