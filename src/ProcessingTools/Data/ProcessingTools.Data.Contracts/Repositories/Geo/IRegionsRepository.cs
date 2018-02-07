// <copyright file="IRegionsRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Repositories.Geo
{
    using ProcessingTools.Models.Contracts.Geo;

    /// <summary>
    /// Regions repository.
    /// </summary>
    public interface IRegionsRepository : IRepositoryAsync<IRegion, IRegionsFilter>, IGeoSynonymisableRepository<IRegion, IRegionSynonym, IRegionSynonymsFilter>
    {
    }
}
