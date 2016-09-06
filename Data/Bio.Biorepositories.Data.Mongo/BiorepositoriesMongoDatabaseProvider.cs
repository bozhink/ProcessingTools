namespace ProcessingTools.Bio.Biorepositories.Data.Mongo
{
    using System.Configuration;
    using Contracts;
    using MongoDB.Bson.Serialization.Conventions;
    using MongoDB.Driver;
    using ProcessingTools.Data.Common.Mongo.Constants;

    public class BiorepositoriesMongoDatabaseProvider : IBiorepositoriesMongoDatabaseProvider
    {
        private const string BiorepositoriesMongoConnectionKey = "BiorepositoriesMongoConnection";
        private const string BiorepositoriesMongoDabaseNameKey = "BiorepositoriesMongoDabaseName";

        private readonly string connectionString;
        private readonly string databaseName;

        public BiorepositoriesMongoDatabaseProvider()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings[BiorepositoriesMongoConnectionKey].ConnectionString;
            this.databaseName = ConfigurationManager.AppSettings[BiorepositoriesMongoDabaseNameKey];
        }

        public IMongoDatabase Create()
        {
            var conventionPack = new ConventionPack();
            conventionPack.Add(new CamelCaseElementNameConvention());
            ConventionRegistry.Register(ConfigurationConstants.CamelCaseConventionPackName, conventionPack, t => true);

            var client = new MongoClient(this.connectionString);
            return client.GetDatabase(this.databaseName);
        }
    }
}
