namespace ProcessingTools.Data.Common.Mongo.Contracts.Repositories
{
    using ProcessingTools.Data.Contracts.Repositories;

    public interface IMongoGenericRepository<T> : ICrudRepository<T>, IMongoSearchableRepository<T>, IMongoCrudRepository<T>, IMongoRepository<T>
        where T : class
    {
    }
}
