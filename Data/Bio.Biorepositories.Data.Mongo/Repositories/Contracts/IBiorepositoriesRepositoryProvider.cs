namespace ProcessingTools.Bio.Biorepositories.Data.Mongo.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IBiorepositoriesRepositoryProvider<T> : IGenericRepositoryProvider<IGenericRepository<T>, T>
        where T : class
    {
    }
}
