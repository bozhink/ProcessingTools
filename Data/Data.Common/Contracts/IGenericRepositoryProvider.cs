namespace ProcessingTools.Data.Common.Contracts
{
    using Repositories.Contracts;

    public interface IGenericRepositoryProvider<TRepository, T>
        where TRepository : IGenericRepository<T>
    {
        TRepository Create();
    }
}
