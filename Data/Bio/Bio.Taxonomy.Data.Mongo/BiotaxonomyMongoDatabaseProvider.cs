namespace ProcessingTools.Bio.Taxonomy.Data.Mongo
{
    using System;
    using Contracts;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Conventions;
    using MongoDB.Driver;
    using ProcessingTools.Data.Common.Mongo.Constants;

    public class BiotaxonomyMongoDatabaseProvider : IBiotaxonomyMongoDatabaseProvider
    {
        private readonly string connectionString;
        private readonly string databaseName;

        public BiotaxonomyMongoDatabaseProvider(string connectionString, string databaseName)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            if (string.IsNullOrWhiteSpace(databaseName))
            {
                throw new ArgumentNullException(nameof(databaseName));
            }

            this.connectionString = connectionString;
            this.databaseName = databaseName;
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
