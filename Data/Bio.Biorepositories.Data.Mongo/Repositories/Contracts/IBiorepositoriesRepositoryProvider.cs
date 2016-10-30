namespace ProcessingTools.Bio.Biorepositories.Data.Mongo.Repositories.Contracts
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IBiorepositoriesRepositoryProvider<T> : IGenericRepositoryProvider<T>
        where T : class
    {
    }
}
