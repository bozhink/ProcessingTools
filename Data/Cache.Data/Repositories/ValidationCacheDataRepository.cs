namespace ProcessingTools.Cache.Data.Repositories
{
    using Contracts;
    using Models;

    using ProcessingTools.Data.Common.Redis.Contracts;
    using ProcessingTools.Data.Common.Redis.Repositories.Factories;

    public class ValidationCacheDataRepository : RedisGenericRepository<ValidationCacheEntity>, IValidationCacheDataRepository
    {
        public ValidationCacheDataRepository()
            : this(new RedisClientProvider())
        {
        }

        public ValidationCacheDataRepository(IRedisClientProvider provider)
            : base(provider)
        {
        }
    }
}