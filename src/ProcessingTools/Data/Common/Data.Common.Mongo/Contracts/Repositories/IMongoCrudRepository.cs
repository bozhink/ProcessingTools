namespace ProcessingTools.Data.Common.Mongo.Contracts.Repositories
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IMongoCrudRepository<T> : ICrudRepository<T>, IMongoRepository<T>
        where T : class
    {
    }
}
