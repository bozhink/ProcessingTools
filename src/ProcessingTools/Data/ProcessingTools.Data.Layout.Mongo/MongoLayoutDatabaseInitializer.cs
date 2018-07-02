// <copyright file="MongoLayoutDatabaseInitializer.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Layout.Mongo
{
    using System;
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using ProcessingTools.Data.Common.Mongo;
    using ProcessingTools.Data.Common.Mongo.Contracts;
    using ProcessingTools.Data.Contracts.Documents;
    using ProcessingTools.Data.Models.Layout.Mongo;

    /// <summary>
    /// MongoDB implementation for <see cref="IDocumentsDatabaseInitializer"/>.
    /// </summary>
    public class MongoLayoutDatabaseInitializer : IDocumentsDatabaseInitializer
    {
        private readonly IMongoDatabase db;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoLayoutDatabaseInitializer"/> class.
        /// </summary>
        /// <param name="databaseProvider">Instance of <see cref="IMongoDatabaseProvider"/>.</param>
        public MongoLayoutDatabaseInitializer(IMongoDatabaseProvider databaseProvider)
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
            await this.GetCollection<FloatObjectTagStyle>().Indexes
                .CreateManyAsync(new CreateIndexModel<FloatObjectTagStyle>[]
                {
                    new CreateIndexModel<FloatObjectTagStyle>(new IndexKeysDefinitionBuilder<FloatObjectTagStyle>().Ascending(b => b.ObjectId))
                })
                .ConfigureAwait(false);

            await this.GetCollection<FloatObjectParseStyle>().Indexes
                .CreateManyAsync(new CreateIndexModel<FloatObjectParseStyle>[]
                {
                    new CreateIndexModel<FloatObjectParseStyle>(new IndexKeysDefinitionBuilder<FloatObjectParseStyle>().Ascending(b => b.ObjectId))
                })
                .ConfigureAwait(false);

            await this.GetCollection<ReferenceTagStyle>().Indexes
                .CreateManyAsync(new CreateIndexModel<ReferenceTagStyle>[]
                {
                    new CreateIndexModel<ReferenceTagStyle>(new IndexKeysDefinitionBuilder<ReferenceTagStyle>().Ascending(b => b.ObjectId))
                })
                .ConfigureAwait(false);

            await this.GetCollection<ReferenceParseStyle>().Indexes
                .CreateManyAsync(new CreateIndexModel<ReferenceParseStyle>[]
                {
                    new CreateIndexModel<ReferenceParseStyle>(new IndexKeysDefinitionBuilder<ReferenceParseStyle>().Ascending(b => b.ObjectId))
                })
                .ConfigureAwait(false);

            await this.GetCollection<JournalStyle>().Indexes
                .CreateManyAsync(new CreateIndexModel<JournalStyle>[]
                {
                    new CreateIndexModel<JournalStyle>(new IndexKeysDefinitionBuilder<JournalStyle>().Ascending(b => b.ObjectId))
                })
                .ConfigureAwait(false);

            return true;
        }

        private IMongoCollection<B> GetCollection<B>()
        {
            return this.db.GetCollection<B>(MongoCollectionNameFactory.Create<B>());
        }
    }
}
