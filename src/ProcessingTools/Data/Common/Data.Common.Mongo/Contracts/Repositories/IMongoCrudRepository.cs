namespace ProcessingTools.Data.Common.Mongo.Contracts.Repositories
{
    using ProcessingTools.Data.Contracts.Repositories;

    public interface IMongoCrudRepository<T> : ICrudRepository<T>, IMongoRepository<T>
        where T : class
    {
    }
}
