namespace ProcessingTools.Data.Common.Mongo
{
    using System;
    using MongoDB.Bson.Serialization.Conventions;
    using MongoDB.Driver;
    using ProcessingTools.Data.Common.Mongo.Constants;
    using ProcessingTools.Data.Common.Mongo.Contracts;

    public class MongoDatabaseProvider : IMongoDatabaseProvider
    {
        private readonly string connectionString;
        private readonly string databaseName;

        public MongoDatabaseProvider(string connectionString, string databaseName)
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
                new CamelCaseElementNameConvention()
            };
            ConventionRegistry.Register(ConfigurationConstants.CamelCaseConventionPackName, conventionPack, t => true);

            var client = new MongoClient(this.connectionString);
            return client.GetDatabase(this.databaseName);
        }
    }
}
