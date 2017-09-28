// <copyright file="IValidationCacheDataRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Repositories.Cache
{
    using ProcessingTools.Data.Contracts.Repositories;
    using ProcessingTools.Models.Contracts.Cache;

    /// <summary>
    /// Validation cache data repository.
    /// </summary>
    public interface IValidationCacheDataRepository : IStringKeyCollectionValuePairsRepository<IValidationCacheEntity>
    {
    }
}
