namespace ProcessingTools.Data.Common.Mongo.Contracts.Repositories
{
    using ProcessingTools.Data.Contracts.Repositories;

    public interface IMongoRepository<T> : IRepository<T>
        where T : class
    {
    }
}
