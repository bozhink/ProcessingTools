namespace ProcessingTools.Data.Common.Mongo.Contracts.Repositories
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IMongoRepository<T> : IRepository<T>
        where T : class
    {
    }
}
