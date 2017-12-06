namespace ProcessingTools.Bio.Biorepositories.Data.Mongo.Contracts.Repositories
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IBiorepositoriesRepositoryProvider<T> : ICrudRepositoryProvider<T>
        where T : class
    {
    }
}
