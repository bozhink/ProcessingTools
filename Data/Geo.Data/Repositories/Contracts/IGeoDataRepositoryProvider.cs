namespace ProcessingTools.Geo.Data.Repositories.Contracts
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IGeoDataRepositoryProvider<T> : ISearchableCountableCrudRepositoryProvider<T>
        where T : class
    {
    }
}
