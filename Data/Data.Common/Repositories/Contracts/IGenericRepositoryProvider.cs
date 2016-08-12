namespace ProcessingTools.Data.Common.Repositories.Contracts
{
    public interface IGenericRepositoryProvider<TRepository, T>
        where TRepository : IGenericRepository<T>
    {
        TRepository Create();
    }
}
