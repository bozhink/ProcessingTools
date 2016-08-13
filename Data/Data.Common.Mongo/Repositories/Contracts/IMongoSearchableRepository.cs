namespace ProcessingTools.Data.Common.Mongo.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IMongoSearchableRepository<T> : IMongoRepository<T>, ISearchableRepository<T>
        where T : class
    {
    }
}
