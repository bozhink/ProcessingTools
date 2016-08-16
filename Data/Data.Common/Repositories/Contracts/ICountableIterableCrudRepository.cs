namespace ProcessingTools.Data.Common.Repositories.Contracts
{
    public interface ICountableIterableCrudRepository<TEntity> : ICountableIterableRepository<TEntity>, IIterableCrudRepository<TEntity>, ICountableCrudRepository<TEntity>
    {
    }
}
