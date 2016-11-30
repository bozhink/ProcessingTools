namespace ProcessingTools.Contracts.Data.Repositories
{
    public interface IRepositoryFactory<TRepository>
        where TRepository : IRepository
    {
        TRepository Create();
    }
}
