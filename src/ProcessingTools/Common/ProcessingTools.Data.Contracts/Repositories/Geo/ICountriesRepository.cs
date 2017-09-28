// <copyright file="ICountriesRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Repositories.Geo
{
    using ProcessingTools.Models.Contracts.Geo;

    /// <summary>
    /// Countries repository.
    /// </summary>
    public interface ICountriesRepository : IRepositoryAsync<ICountry, ICountriesFilter>, IGeoSynonymisableRepository<ICountry, ICountrySynonym, ICountrySynonymsFilter>
    {
    }
}
