namespace ProcessingTools.Data.Common.Entity.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IEntityGenericRepository<T> : IGenericRepository<T>, IEntityCrudRepository<T>, IEntitySearchableRepository<T>, IEntityRepository<T>
        where T : class
    {
    }
}
