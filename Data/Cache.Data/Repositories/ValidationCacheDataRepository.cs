﻿namespace ProcessingTools.Cache.Data.Repositories
{
    using Contracts;
    using Models;

    using ProcessingTools.Data.Common.Redis.Contracts;
    using ProcessingTools.Data.Common.Redis.Repositories;

    public class ValidationCacheDataRepository : RedisGenericRepository<ValidationCacheEntity>, IValidationCacheDataRepository
    {
        public ValidationCacheDataRepository(IRedisClientProvider provider)
            : base(provider)
        {
        }
    }
}