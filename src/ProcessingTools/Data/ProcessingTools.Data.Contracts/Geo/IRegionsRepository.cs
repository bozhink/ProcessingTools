// <copyright file="IRegionsRepository.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Geo
{
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Regions repository.
    /// </summary>
    public interface IRegionsRepository : IRepositoryAsync<IRegion, IRegionsFilter>, IGeoSynonymisableRepository<IRegion, IRegionSynonym, IRegionSynonymsFilter>
    {
    }
}
