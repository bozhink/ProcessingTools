namespace ProcessingTools.Bio.Biorepositories.Data.Mongo
{
    using System.Configuration;
    using Contracts;
    using MongoDB.Bson.Serialization.Conventions;
    using MongoDB.Driver;

    public class BiorepositoriesMongoDatabaseProvider : IBiorepositoriesMongoDatabaseProvider
    {
        private const string BiorepositoriesMongoConnectionKey = "BiorepositoriesMongoConnection";
        private const string BiorepositoriesMongoDabaseNameKey = "BiorepositoriesMongoDabaseName";

        private const string CamelCaseConventionPackName = "camelCase";

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
            ConventionRegistry.Register(CamelCaseConventionPackName, conventionPack, t => true);

            var client = new MongoClient(this.connectionString);
            return client.GetDatabase(this.databaseName);
        }
    }
}
