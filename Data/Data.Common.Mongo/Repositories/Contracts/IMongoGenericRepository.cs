namespace ProcessingTools.Data.Common.Mongo.Repositories.Contracts
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IMongoGenericRepository<T> : IGenericRepository<T>, IMongoSearchableRepository<T>, IMongoCrudRepository<T>, IMongoRepository<T>
        where T : class
    {
    }
}
