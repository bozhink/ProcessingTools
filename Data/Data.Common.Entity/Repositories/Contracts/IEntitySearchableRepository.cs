namespace ProcessingTools.Data.Common.Entity.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IEntitySearchableRepository<T> : ISearchableRepository<T>, IEntityRepository<T>
        where T : class
    {
    }
}
