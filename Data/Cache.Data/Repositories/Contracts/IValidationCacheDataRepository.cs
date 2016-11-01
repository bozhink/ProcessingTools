namespace ProcessingTools.Cache.Data.Repositories.Contracts
{
    using ProcessingTools.Cache.Data.Models;
    using ProcessingTools.Data.Common.Redis.Contracts.Repositories;

    public interface IValidationCacheDataRepository : IRedisGenericRepository<ValidationCacheEntity>
    {
    }
}