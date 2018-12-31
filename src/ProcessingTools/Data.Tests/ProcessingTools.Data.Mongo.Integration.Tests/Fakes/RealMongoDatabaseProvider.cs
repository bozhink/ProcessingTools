// <copyright file="RealMongoDatabaseProvider.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Mongo.Integration.Tests.Fakes
{
    using MongoDB.Driver;
    using ProcessingTools.Data.Mongo;

    /// <summary>
    /// Real MongoDB database provider.
    /// </summary>
    public class RealMongoDatabaseProvider : IMongoDatabaseProvider
    {
        private const string ConnectionString = "mongodb://localhost";
        private readonly string databaseName;

        /// <summary>
        /// Initializes a new instance of the <see cref="RealMongoDatabaseProvider"/> class.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        public RealMongoDatabaseProvider(string databaseName)
        {
            this.databaseName = databaseName;
        }

        /// <inheritdoc/>
        public IMongoDatabase Create()
        {
            var client = new MongoClient(ConnectionString);
            return client.GetDatabase(this.databaseName);
        }
    }
}
