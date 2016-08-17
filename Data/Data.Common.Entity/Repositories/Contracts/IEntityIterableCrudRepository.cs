namespace ProcessingTools.Data.Common.Entity.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IEntityIterableCrudRepository<TEntity> : IIterableCrudRepository<TEntity>, IEntityCrudRepository<TEntity>
    {
    }
}
