namespace ProcessingTools.Contracts.Data.Repositories
{
    public interface ICrudRepositoryProvider<T> : IRepositoryProvider<ICrudRepository<T>>
    {
    }
}
