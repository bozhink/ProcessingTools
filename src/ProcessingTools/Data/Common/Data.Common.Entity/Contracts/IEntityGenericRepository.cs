namespace ProcessingTools.Data.Common.Entity.Contracts
{
    public interface IEntityGenericRepository<T> : IEntityCrudRepository<T>, IEntitySearchableRepository<T>
        where T : class
    {
    }
}
