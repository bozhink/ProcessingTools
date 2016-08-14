namespace ProcessingTools.Data.Common.Repositories.Contracts
{
    public interface IGenericRepository<TEntity> : ICountableRepository<TEntity>, ISearchableRepository<TEntity>, IIterableRepository<TEntity>, ICrudRepository<TEntity>, IRepository<TEntity>
    {
    }
}
