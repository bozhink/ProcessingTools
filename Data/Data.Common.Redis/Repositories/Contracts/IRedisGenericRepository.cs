namespace ProcessingTools.Data.Common.Redis.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Models.Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IRedisGenericRepository<TEntity> : ISimpleGenericRepository<TEntity>
        where TEntity : IEntity
    {
    }
}
