// <copyright file="MongoDocumentsDatabaseInitializer.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Documents.Mongo
{
    using System;
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using ProcessingTools.Data.Common.Mongo;
    using ProcessingTools.Data.Common.Mongo.Contracts;
    using ProcessingTools.Data.Contracts.Documents;
    using ProcessingTools.Data.Models.Documents.Mongo;

    /// <summary>
    /// MongoDB implementation for <see cref="IDocumentsDatabaseInitializer"/>.
    /// </summary>
    public class MongoDocumentsDatabaseInitializer : IDocumentsDatabaseInitializer
    {
        private readonly IMongoDatabase db;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDocumentsDatabaseInitializer"/> class.
        /// </summary>
        /// <param name="databaseProvider">Instance of <see cref="IMongoDatabaseProvider"/>.</param>
        public MongoDocumentsDatabaseInitializer(IMongoDatabaseProvider databaseProvider)
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
            await this.GetCollection<Article>().Indexes
                .CreateManyAsync(new CreateIndexModel<Article>[]
                {
                    new CreateIndexModel<Article>(new IndexKeysDefinitionBuilder<Article>().Ascending(b => b.ObjectId)),
                    new CreateIndexModel<Article>(new IndexKeysDefinitionBuilder<Article>().Ascending(b => b.JournalId))
                })
                .ConfigureAwait(false);

            await this.GetCollection<Document>().Indexes
                .CreateManyAsync(new CreateIndexModel<Document>[]
                {
                    new CreateIndexModel<Document>(new IndexKeysDefinitionBuilder<Document>().Ascending(b => b.ObjectId)),
                    new CreateIndexModel<Document>(new IndexKeysDefinitionBuilder<Document>().Ascending(b => b.ArticleId)),
                    new CreateIndexModel<Document>(new IndexKeysDefinitionBuilder<Document>().Ascending(b => b.ArticleId).Ascending(b => b.IsFinal))
                })
                .ConfigureAwait(false);

            await this.GetCollection<DocumentContent>().Indexes
                .CreateManyAsync(new CreateIndexModel<DocumentContent>[]
                {
                    new CreateIndexModel<DocumentContent>(new IndexKeysDefinitionBuilder<DocumentContent>().Ascending(b => b.DocumentId))
                })
                .ConfigureAwait(false);

            await this.GetCollection<File>().Indexes
                .CreateManyAsync(new CreateIndexModel<File>[]
                {
                    new CreateIndexModel<File>(new IndexKeysDefinitionBuilder<File>().Ascending(b => b.ObjectId))
                })
                .ConfigureAwait(false);

            await this.GetCollection<Journal>().Indexes
                .CreateManyAsync(new CreateIndexModel<Journal>[]
                {
                    new CreateIndexModel<Journal>(new IndexKeysDefinitionBuilder<Journal>().Ascending(b => b.ObjectId)),
                    new CreateIndexModel<Journal>(new IndexKeysDefinitionBuilder<Journal>().Ascending(b => b.PublisherId)),
                    new CreateIndexModel<Journal>(new IndexKeysDefinitionBuilder<Journal>().Ascending(b => b.ObjectId).Ascending(b => b.Name).Ascending(b => b.AbbreviatedName)),
                })
                .ConfigureAwait(false);

            await this.GetCollection<JournalMeta>().Indexes
                .CreateManyAsync(new CreateIndexModel<JournalMeta>[]
                {
                    new CreateIndexModel<JournalMeta>(new IndexKeysDefinitionBuilder<JournalMeta>().Ascending(b => b.JournalTitle))
                })
                .ConfigureAwait(false);

            await this.GetCollection<Publisher>().Indexes
                .CreateManyAsync(new CreateIndexModel<Publisher>[]
                {
                    new CreateIndexModel<Publisher>(new IndexKeysDefinitionBuilder<Publisher>().Ascending(b => b.ObjectId)),
                    new CreateIndexModel<Publisher>(new IndexKeysDefinitionBuilder<Publisher>().Ascending(b => b.ObjectId).Ascending(b => b.Name).Ascending(b => b.AbbreviatedName))
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
