namespace ProcessingTools.Bio.Data.Repositories.Contracts
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IBioDataRepositoryProvider<T> : IGenericRepositoryProvider<T>
        where T : class
    {
    }
}
