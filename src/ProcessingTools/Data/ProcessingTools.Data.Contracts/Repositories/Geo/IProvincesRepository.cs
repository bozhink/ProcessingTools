// <copyright file="IProvincesRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Repositories.Geo
{
    using ProcessingTools.Models.Contracts.Geo;

    /// <summary>
    /// Provinces repository.
    /// </summary>
    public interface IProvincesRepository : IRepositoryAsync<IProvince, IProvincesFilter>, IGeoSynonymisableRepository<IProvince, IProvinceSynonym, IProvinceSynonymsFilter>
    {
    }
}
