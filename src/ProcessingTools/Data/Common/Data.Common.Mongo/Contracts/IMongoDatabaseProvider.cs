namespace ProcessingTools.Data.Common.Mongo.Contracts
{
    using MongoDB.Driver;
    using ProcessingTools.Data.Contracts;

    public interface IMongoDatabaseProvider : IDatabaseProvider<IMongoDatabase>
    {
    }
}
