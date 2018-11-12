// <copyright file="MongoDatabaseProvider.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Mongo.Common
{
    using System;
    using Constants;
    using Contracts;
    using MongoDB.Bson.Serialization.Conventions;
    using MongoDB.Driver;

    /// <summary>
    /// MongoDB database provider.
    /// </summary>
    public class MongoDatabaseProvider : IMongoDatabaseProvider
    {
        private readonly string connectionString;
        private readonly string databaseName;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDatabaseProvider"/> class.
        /// </summary>
        /// <param name="connectionString">Connection string.</param>
        /// <param name="databaseName">Database name.</param>
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

        /// <inheritdoc/>
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
