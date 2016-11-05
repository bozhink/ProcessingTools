namespace ProcessingTools.Bio.Data.Repositories.Contracts
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IBioDataRepositoryProvider<T> : ISearchableCountableCrudRepositoryProvider<T>
        where T : class
    {
    }
}
