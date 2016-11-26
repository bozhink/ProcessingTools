namespace ProcessingTools.Bio.Biorepositories.Data.Mongo.Repositories.Contracts
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IBiorepositoriesRepositoryProvider<T> : ISearchableCountableCrudRepositoryProvider<T>
        where T : class
    {
    }
}
