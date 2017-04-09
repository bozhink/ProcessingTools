namespace ProcessingTools.Contracts.Data.Repositories
{
    public interface ICrudRepository<T> : IAddableRepository<T>, IDeletableRepository<T>, IUpdatableRepository<T>, ISearchableRepository<T>, IRepository<T>
    {
    }
}
