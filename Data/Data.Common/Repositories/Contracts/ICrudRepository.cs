namespace ProcessingTools.Data.Common.Repositories.Contracts
{
    public interface ICrudRepository<T> : IUpdatableRepository<T>, ISearchableRepository<T>
    {
    }
}
