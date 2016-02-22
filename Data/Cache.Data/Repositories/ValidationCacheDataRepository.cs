namespace ProcessingTools.Cache.Data.Repositories
{
    using Contracts;
    using Models.Contracts;
    using ProcessingTools.Data.Common.Redis.Contracts;
    using ProcessingTools.Data.Common.Redis.Repositories.Factories;

    public class ValidationCacheDataRepository : RedisGenericRepository<IValidationCacheEntity>, IValidationCacheDataRepository
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