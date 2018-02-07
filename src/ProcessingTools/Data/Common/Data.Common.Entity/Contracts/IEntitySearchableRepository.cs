namespace ProcessingTools.Data.Common.Entity.Contracts
{
    using ProcessingTools.Data.Contracts;

    public interface IEntitySearchableRepository<T> : ISearchableRepository<T>, IEntityRepository<T>
        where T : class
    {
    }
}
