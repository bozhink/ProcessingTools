namespace ProcessingTools.Data.Common.Mongo.Tests.Fakes
{
    using Data.Mongo.Common.Contracts;
    using MongoDB.Driver;

    public class RealMongoDatabaseProvider : IMongoDatabaseProvider
    {
        private const string ConnectionString = "mongodb://localhost";
        private readonly string databaseName;

        public RealMongoDatabaseProvider(string databaseName)
        {
            this.databaseName = databaseName;
        }

        public IMongoDatabase Create()
        {
            var client = new MongoClient(ConnectionString);
            return client.GetDatabase(this.databaseName);
        }
    }
}