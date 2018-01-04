namespace ProcessingTools.Data.Common.Entity.Repositories.Contracts
{
    public interface IEntityGenericRepository<T> : IEntityCrudRepository<T>, IEntitySearchableRepository<T>
        where T : class
    {
    }
}
