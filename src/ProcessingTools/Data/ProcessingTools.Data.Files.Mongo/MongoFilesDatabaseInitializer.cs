// <copyright file="MongoFilesDatabaseInitializer.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Files.Mongo
{
    using System;
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using ProcessingTools.Data.Common.Mongo;
    using ProcessingTools.Data.Common.Mongo.Contracts;
    using ProcessingTools.Data.Contracts.Files;
    using ProcessingTools.Data.Models.Files.Mongo;

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
