namespace ProcessingTools.Bio.Biorepositories.Data.Mongo
{
    using System.Configuration;
    using Contracts;
    using MongoDB.Driver;

    public class BiorepositoriesMongoDatabaseProvider : IBiorepositoriesMongoDatabaseProvider
    {
        private readonly string serverUrl;
        private readonly string databaseName;

        public BiorepositoriesMongoDatabaseProvider()
        {
            this.serverUrl = ConfigurationManager.AppSettings["BiorepositoriesMongoDbServerUrl"];
            this.databaseName = ConfigurationManager.AppSettings["BiorepositoriesMongoDabaseName"];
        }

        public IMongoDatabase Create()
        {
            var client = new MongoClient(this.serverUrl);
            return client.GetDatabase(this.databaseName);
        }
    }
}
