namespace ProcessingTools.Data.Common.Repositories.Contracts
{
    public interface IGenericRepository<TEntity> : ICountableIterableCrudRepository<TEntity>, ISearchableRepository<TEntity>, IRepository<TEntity>
    {
    }
}
