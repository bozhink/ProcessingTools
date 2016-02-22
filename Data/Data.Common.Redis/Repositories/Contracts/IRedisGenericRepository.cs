namespace ProcessingTools.Data.Common.Redis.Repositories.Contracts
{
    using Models.Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IRedisGenericRepository<TEntity> : IGenericRepository<string, int, TEntity>
        where TEntity : IRedisEntity
    {
    }
}
