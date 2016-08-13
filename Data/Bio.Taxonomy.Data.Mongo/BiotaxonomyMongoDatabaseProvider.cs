namespace ProcessingTools.Bio.Taxonomy.Data.Mongo
{
    using System.Configuration;
    using Contracts;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Conventions;
    using MongoDB.Driver;
    using ProcessingTools.Data.Common.Mongo.Constants;

    public class BiotaxonomyMongoDatabaseProvider : IBiotaxonomyMongoDatabaseProvider
    {
        private const string BiotaxonomyMongoConnectionKey = "BiotaxonomyMongoConnection";
        private const string BiotaxonomyMongoDabaseNameKey = "BiotaxonomyMongoDabaseName";

        private readonly string connectionString;
        private readonly string databaseName;

        public BiotaxonomyMongoDatabaseProvider()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings[BiotaxonomyMongoConnectionKey].ConnectionString;
            this.databaseName = ConfigurationManager.AppSettings[BiotaxonomyMongoDabaseNameKey];
        }

        public IMongoDatabase Create()
        {
            var conventionPack = new ConventionPack
            {
                new CamelCaseElementNameConvention(),
                new EnumRepresentationConvention(BsonType.String)
            };

            ConventionRegistry.Register(ConfigurationConstants.EnumStringConventionPackName, conventionPack, t => true);

            var client = new MongoClient(this.connectionString);
            return client.GetDatabase(this.databaseName);
        }
    }
}
