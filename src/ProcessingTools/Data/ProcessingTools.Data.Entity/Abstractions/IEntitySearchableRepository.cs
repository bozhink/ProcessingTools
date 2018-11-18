namespace ProcessingTools.Data.Entity.Abstractions
{
    using ProcessingTools.Data.Contracts;

    public interface IEntitySearchableRepository<T> : ISearchableRepository<T>, IEntityRepository<T>
        where T : class
    {
    }
}
