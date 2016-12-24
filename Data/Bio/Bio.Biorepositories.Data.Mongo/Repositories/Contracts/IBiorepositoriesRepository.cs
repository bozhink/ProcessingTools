namespace ProcessingTools.Bio.Biorepositories.Data.Mongo.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Mongo.Contracts.Repositories;

    public interface IBiorepositoriesRepository<T> : IMongoGenericRepository<T>
        where T : class
    {
    }
}
