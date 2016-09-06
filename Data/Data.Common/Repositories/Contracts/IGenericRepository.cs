namespace ProcessingTools.Data.Common.Repositories.Contracts
{
    public interface IGenericRepository<T> : ICountableCrudRepository<T>, ISearchableRepository<T>, IRepository<T>
    {
    }
}
