namespace ProcessingTools.Data.Common.Entity.Repositories.Contracts
{
    using ProcessingTools.Data.Contracts.Repositories;

    public interface IEntitySearchableRepository<T> : ISearchableRepository<T>, IEntityRepository<T>
        where T : class
    {
    }
}
