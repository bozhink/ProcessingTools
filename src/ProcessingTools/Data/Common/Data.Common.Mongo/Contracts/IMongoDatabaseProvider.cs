namespace ProcessingTools.Data.Common.Mongo.Contracts
{
    using MongoDB.Driver;
    using ProcessingTools.Contracts.Data;

    public interface IMongoDatabaseProvider : IDatabaseProvider<IMongoDatabase>
    {
    }
}
