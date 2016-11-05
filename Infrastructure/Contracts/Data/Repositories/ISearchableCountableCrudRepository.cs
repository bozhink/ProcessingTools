namespace ProcessingTools.Contracts.Data.Repositories
{
    public interface ISearchableCountableCrudRepository<T> : ICountableCrudRepository<T>, ISearchableRepository<T>, IRepository<T>
    {
    }
}
