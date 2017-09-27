// <copyright file="IValidationCacheDataRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Cache.Repositories
{
    using ProcessingTools.Contracts.Data.Cache.Models;
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IValidationCacheDataRepository : IStringKeyCollectionValuePairsRepository<IValidationCacheEntity>
    {
    }
}
