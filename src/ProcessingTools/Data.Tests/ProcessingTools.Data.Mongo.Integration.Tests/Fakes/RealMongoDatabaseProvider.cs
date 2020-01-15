// <copyright file="RealMongoDatabaseProvider.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Mongo.Integration.Tests.Fakes
{
    using MongoDB.Driver;

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

        /// <inheritdoc/>
        public IMongoCollection<T> GetCollection<T>(IMongoDatabase db)
        {
            if (db is null)
            {
                throw new System.ArgumentNullException(nameof(db));
            }

            string collectionName = MongoCollectionNameFactory.Create<T>();
            return db.GetCollection<T>(collectionName);
        }

        /// <inheritdoc/>
        public IMongoCollection<T> GetCollection<T>(IMongoDatabase db, MongoCollectionSettings settings)
        {
            if (db is null)
            {
                throw new System.ArgumentNullException(nameof(db));
            }

            if (settings is null)
            {
                throw new System.ArgumentNullException(nameof(settings));
            }

            string collectionName = MongoCollectionNameFactory.Create<T>();
            return db.GetCollection<T>(collectionName, settings);
        }
    }
}
