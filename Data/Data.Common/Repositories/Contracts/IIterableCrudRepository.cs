namespace ProcessingTools.Data.Common.Repositories.Contracts
{
    public interface IIterableCrudRepository<TEntity> : IIterableRepository<TEntity>, ICrudRepository<TEntity>
    {
    }
}
