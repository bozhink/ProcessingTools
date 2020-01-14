// <copyright file="IProvincesRepository.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Geo
{
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Provinces repository.
    /// </summary>
    public interface IProvincesRepository : IRepositoryAsync<IProvince, IProvincesFilter>, IGeoSynonymisableRepository<IProvince, IProvinceSynonym, IProvinceSynonymsFilter>
    {
    }
}
