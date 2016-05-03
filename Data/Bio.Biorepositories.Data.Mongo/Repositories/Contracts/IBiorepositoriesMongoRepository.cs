namespace ProcessingTools.Bio.Biorepositories.Data.Mongo.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Mongo.Repositories.Contracts;

    public interface IBiorepositoriesMongoRepository<T> : IMongoGenericRepository<T>
        where T : class
    {
    }
}
