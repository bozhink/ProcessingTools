namespace ProcessingTools.Data.Common.Repositories.Contracts
{
    public interface ICrudRepositoryProvider<T> : IRepositoryProvider<ICrudRepository<T>>
    {
    }
}
