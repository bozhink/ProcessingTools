// <copyright file="MongoFilesDatabaseInitializer.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Mongo.Files
{
    using System;
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using ProcessingTools.Contracts.Data;
    using ProcessingTools.Data.Models.Mongo.Files;

    /// <summary>
    /// MongoDB implementation for <see cref="IFilesDatabaseInitializer"/>.
    /// </summary>
    public class MongoFilesDatabaseInitializer : IFilesDatabaseInitializer
    {
        private readonly IMongoDatabase db;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoFilesDatabaseInitializer"/> class.
        /// </summary>
        /// <param name="db">Instance of <see cref="IMongoDatabase"/>.</param>
        public MongoFilesDatabaseInitializer(IMongoDatabase db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
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
                    new CreateIndexModel<Mediatype>(new IndexKeysDefinitionBuilder<Mediatype>().Ascending(b => b.Extension).Ascending(b => b.MimeType).Ascending(b => b.MimeSubtype)),
                })
                .ConfigureAwait(false);

            return true;
        }

        private IMongoCollection<T> GetCollection<T>() => this.db.GetCollection<T>(MongoCollectionNameFactory.Create<T>());
    }
}
