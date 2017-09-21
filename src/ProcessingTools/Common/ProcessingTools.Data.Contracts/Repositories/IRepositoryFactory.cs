namespace ProcessingTools.Contracts.Data.Repositories
{
    public interface IRepositoryFactory<out TRepository>
        where TRepository : IRepository
    {
        TRepository Create();
    }
}
