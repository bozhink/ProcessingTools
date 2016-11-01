namespace ProcessingTools.Data.Common.Redis.Contracts.Repositories
{
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IRedisGenericRepository<TEntity> : IGenericContextRepository<string, TEntity>
        where TEntity : IEntity
    {
    }
}
