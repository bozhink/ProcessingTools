namespace ProcessingTools.Bio.Biorepositories.Data.Mongo.Contracts.Repositories
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IBiorepositoriesRepositoryProvider<T> : ISearchableCountableCrudRepositoryProvider<T>
        where T : class
    {
    }
}
