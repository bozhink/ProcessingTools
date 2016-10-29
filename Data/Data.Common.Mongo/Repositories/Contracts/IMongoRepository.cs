namespace ProcessingTools.Data.Common.Mongo.Repositories.Contracts
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IMongoRepository<T> : IRepository<T>
        where T : class
    {
    }
}
