namespace ProcessingTools.Data.Common.Mongo.Repositories.Contracts
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IMongoSearchableRepository<T> : ISearchableRepository<T>, IMongoRepository<T>
        where T : class
    {
    }
}
