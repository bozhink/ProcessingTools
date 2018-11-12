// <copyright file="MongoFilesDatabaseInitializer.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Mongo.Files
{
    using System;
    using System.Threading.Tasks;
    using Common;
    using Common.Contracts;
    using Contracts.Files;
    using Models.Mongo.Files;
    using MongoDB.Driver;

    /// <summary>
    /// MongoDB implementation for <see cref="IFilesDatabaseInitializer"/>.
    /// </summary>
    public class MongoFilesDatabaseInitializer : IFilesDatabaseInitializer
    {
        private readonly IMongoDatabase db;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoFilesDatabaseInitializer"/> class.
        /// </summary>
        /// <param name="databaseProvider">Instance of <see cref="IMongoDatabaseProvider"/>.</param>
        public MongoFilesDatabaseInitializer(IMongoDatabaseProvider databaseProvider)
        {
            if (databaseProvider == null)
            {
                throw new ArgumentNullException(nameof(databaseProvider));
            }

            this.db = databaseProvider.Create();
        }

        /// <inheritdoc/>
        public async Task<object> InitializeAsync()
        {
            await this.GetCollection<Mediatype>().Indexes
                .CreateManyAsync(new CreateIndexModel<Mediatype>[]
                {
                    new CreateIndexModel<Mediatype>(new IndexKeysDefinitionBuilder<Mediatype>().Ascending(b => b.ObjectId)),
                    new CreateIndexModel<Mediatype>(new IndexKeysDefinitionBuilder<Mediatype>().Ascending(b => b.MimeType)),
                    new CreateIndexModel<Mediatype>(new IndexKeysDefinitionBuilder<Mediatype>().Ascending(b => b.MimeSubtype)),
                    new CreateIndexModel<Mediatype>(new IndexKeysDefinitionBuilder<Mediatype>().Ascending(b => b.Extension).Ascending(b => b.MimeType).Ascending(b => b.MimeSubtype))
                })
                .ConfigureAwait(false);

            return true;
        }

        private IMongoCollection<T> GetCollection<T>() => this.db.GetCollection<T>(MongoCollectionNameFactory.Create<T>());
    }
}
