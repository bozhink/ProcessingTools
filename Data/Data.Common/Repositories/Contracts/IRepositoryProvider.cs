namespace ProcessingTools.Data.Common.Repositories.Contracts
{
    public interface IRepositoryProvider<TRepository>
    {
        TRepository Create();
    }
}
