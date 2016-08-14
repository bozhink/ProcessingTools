namespace ProcessingTools.Data.Common.Mongo.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IMongoIterableRepository<T> : IIterableRepository<T>
        where T : class
    {
    }
}
