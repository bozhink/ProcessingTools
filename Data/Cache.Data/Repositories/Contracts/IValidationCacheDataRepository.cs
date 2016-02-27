namespace ProcessingTools.Cache.Data.Repositories.Contracts
{
    using ProcessingTools.Cache.Data.Models;
    using ProcessingTools.Data.Common.Redis.Repositories.Contracts;

    public interface IValidationCacheDataRepository : IRedisGenericRepository<ValidationCacheEntity>
    {
    }
}