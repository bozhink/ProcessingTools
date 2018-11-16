namespace ProcessingTools.Data.Entity.Abstractions
{
    public interface IEntityGenericRepository<T> : IEntityCrudRepository<T>, IEntitySearchableRepository<T>
        where T : class
    {
    }
}
