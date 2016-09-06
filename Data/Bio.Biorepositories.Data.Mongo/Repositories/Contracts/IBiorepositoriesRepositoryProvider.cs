namespace ProcessingTools.Bio.Biorepositories.Data.Mongo.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IBiorepositoriesRepositoryProvider<T> : IGenericRepositoryProvider<T>
        where T : class
    {
    }
}
