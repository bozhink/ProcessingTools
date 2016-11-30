namespace ProcessingTools.Contracts.Data.Repositories
{
    public interface IRepositoryProvider<TRepository>
        where TRepository : IRepository
    {
        TRepository Create();
    }
}
