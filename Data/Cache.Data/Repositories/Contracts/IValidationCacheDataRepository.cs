namespace ProcessingTools.Cache.Data.Repositories.Contracts
{
    using ProcessingTools.Cache.Data.Models.Contracts;
    using ProcessingTools.Data.Common.Redis.Repositories.Contracts;

    public interface IValidationCacheDataRepository : IRedisGenericRepository<IValidationCacheEntity>
    {
    }
}