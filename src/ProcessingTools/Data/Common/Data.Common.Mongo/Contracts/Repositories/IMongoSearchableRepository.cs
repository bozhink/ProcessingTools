namespace ProcessingTools.Data.Common.Mongo.Contracts.Repositories
{
    using ProcessingTools.Data.Contracts.Repositories;

    public interface IMongoSearchableRepository<T> : ISearchableRepository<T>, IMongoRepository<T>
        where T : class
    {
    }
}
