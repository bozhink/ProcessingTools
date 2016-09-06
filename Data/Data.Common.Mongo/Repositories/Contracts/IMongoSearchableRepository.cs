namespace ProcessingTools.Data.Common.Mongo.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IMongoSearchableRepository<T> : ISearchableRepository<T>, IMongoRepository<T>
        where T : class
    {
    }
}
