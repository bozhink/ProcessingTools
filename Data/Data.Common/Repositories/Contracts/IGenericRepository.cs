namespace ProcessingTools.Data.Common.Repositories.Contracts
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IGenericRepository<T> : ICountableCrudRepository<T>, ISearchableRepository<T>, IRepository<T>
    {
    }
}
