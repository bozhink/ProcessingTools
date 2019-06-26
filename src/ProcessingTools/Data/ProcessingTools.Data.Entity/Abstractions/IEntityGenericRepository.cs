namespace ProcessingTools.Data.Entity.Abstractions
{
    public interface IEntityGenericRepository<T> : IEntityCrudRepository<T>
        where T : class
    {
    }
}
