namespace ProcessingTools.Bio.Biorepositories.Data.Mongo.Contracts.Repositories
{
    using ProcessingTools.Data.Common.Mongo.Contracts.Repositories;

    public interface IBiorepositoriesRepository<T> : IMongoGenericRepository<T>
        where T : class
    {
    }
}
