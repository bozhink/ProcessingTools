namespace ProcessingTools.Data.Common.Mongo.Contracts.Repositories
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IMongoSearchableRepository<T> : ISearchableRepository<T>, IMongoRepository<T>
        where T : class
    {
    }
}
