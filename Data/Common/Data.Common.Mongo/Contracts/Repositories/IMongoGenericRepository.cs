namespace ProcessingTools.Data.Common.Mongo.Contracts.Repositories
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IMongoGenericRepository<T> : ICrudRepository<T>, IMongoSearchableRepository<T>, IMongoCrudRepository<T>, IMongoRepository<T>
        where T : class
    {
    }
}
