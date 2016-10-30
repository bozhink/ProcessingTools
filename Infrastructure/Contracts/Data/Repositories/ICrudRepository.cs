namespace ProcessingTools.Contracts.Data.Repositories
{
    public interface ICrudRepository<T> : IUpdatableRepository<T>, ISearchableRepository<T>
    {
    }
}
