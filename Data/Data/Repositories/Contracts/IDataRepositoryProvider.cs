namespace ProcessingTools.Data.Repositories.Contracts
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IDataRepositoryProvider<T> : IGenericRepositoryProvider<T>
        where T : class
    {
    }
}
