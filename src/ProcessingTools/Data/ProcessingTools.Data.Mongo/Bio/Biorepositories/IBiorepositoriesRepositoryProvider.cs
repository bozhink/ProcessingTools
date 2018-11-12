namespace ProcessingTools.Bio.Biorepositories.Data.Mongo.Contracts.Repositories
{
    using ProcessingTools.Data.Contracts;

    public interface IBiorepositoriesRepositoryProvider<T> : ICrudRepositoryProvider<T>
        where T : class
    {
    }
}
