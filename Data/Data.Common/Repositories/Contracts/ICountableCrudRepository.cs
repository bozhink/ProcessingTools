namespace ProcessingTools.Data.Common.Repositories.Contracts
{
    public interface ICountableCrudRepository<TEntity> : ICountableRepository<TEntity>, ICrudRepository<TEntity>
    {
    }
}
