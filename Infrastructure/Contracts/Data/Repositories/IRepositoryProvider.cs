namespace ProcessingTools.Contracts.Data.Repositories
{
    public interface IRepositoryProvider<TRepository>
    {
        TRepository Create();
    }
}
