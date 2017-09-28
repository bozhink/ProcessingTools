namespace ProcessingTools.Bio.Biorepositories.Data.Mongo.Contracts.Repositories
{
    using ProcessingTools.Data.Contracts.Repositories;

    public interface IBiorepositoriesRepositoryProvider<T> : ICrudRepositoryProvider<T>
        where T : class
    {
    }
}
