namespace ProcessingTools.Data.Common.Redis.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Models.Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IRedisGenericRepository<TEntity> : ISimpleGenericContextRepository<TEntity>
        where TEntity : IEntity
    {
    }
}
