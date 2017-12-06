// <copyright file="IValidationCacheDataRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Repositories.Cache
{
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Contracts.Models.Cache;

    /// <summary>
    /// Validation cache data repository.
    /// </summary>
    public interface IValidationCacheDataRepository : IStringKeyCollectionValuePairsRepository<IValidationCacheModel>
    {
    }
}
