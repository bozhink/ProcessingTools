// <copyright file="IDistrictsRepository.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Geo
{
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Districts repository.
    /// </summary>
    public interface IDistrictsRepository : IRepositoryAsync<IDistrict, IDistrictsFilter>, IGeoSynonymisableRepository<IDistrict, IDistrictSynonym, IDistrictSynonymsFilter>
    {
    }
}
