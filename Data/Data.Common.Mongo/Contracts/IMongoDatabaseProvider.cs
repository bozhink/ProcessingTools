namespace ProcessingTools.Data.Common.Mongo.Contracts
{
    using MongoDB.Driver;
    using ProcessingTools.Data.Common.Contracts;

    public interface IMongoDatabaseProvider : IDatabaseProvider<IMongoDatabase>
    {
    }
}