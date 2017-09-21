namespace ProcessingTools.Contracts.Data.Repositories
{
    public interface IRepositoryProvider<out TRepository>
        where TRepository : IRepository
    {
        TRepository Create();
    }
}
