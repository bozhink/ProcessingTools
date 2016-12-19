namespace ProcessingTools.Contracts.Data.Repositories
{
    public interface ICrudRepository<T> : ISavabaleRepository, IAddableRepository<T>, IDeletableRepository<T>, IUpdatableRepository<T>, ISearchableRepository<T>, IRepository<T>
    {
    }
}
