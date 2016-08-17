namespace ProcessingTools.Data.Common.Entity.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IEntityGenericRepository<TEntity> : IGenericRepository<TEntity>, IEntityCountableIterableCrudRepository<TEntity>
        where TEntity : class
    {
    }
}
