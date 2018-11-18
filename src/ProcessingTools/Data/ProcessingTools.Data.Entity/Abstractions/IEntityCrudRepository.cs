namespace ProcessingTools.Data.Entity.Abstractions
{
    using ProcessingTools.Data.Contracts;

    public interface IEntityCrudRepository<T> : ICrudRepository<T>, IEntityRepository<T>
        where T : class
    {
    }
}
