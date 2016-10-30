namespace ProcessingTools.Contracts.Data.Repositories
{
    public interface ISearchableRepositoryProvider<T> : IRepositoryProvider<ISearchableRepository<T>>
    {
    }
}
