// <copyright file="MongoJournalMetaDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Documents.Mongo
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using ProcessingTools.Data.Common.Mongo;
    using ProcessingTools.Data.Common.Mongo.Contracts;
    using ProcessingTools.Data.Contracts.Documents;
    using ProcessingTools.Data.Models.Documents.Mongo;
    using ProcessingTools.Models.Contracts.Documents;

    /// <summary>
    /// MongoDB implementation of Journals meta data access object.
    /// </summary>
    public class MongoJournalMetaDataAccessObject : IJournalMetaDataAccessObject
    {
        private readonly IMongoDatabase db;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoJournalMetaDataAccessObject"/> class.
        /// </summary>
        /// <param name="databaseProvider">Instance of <see cref="IMongoDatabaseProvider"/>.</param>
        public MongoJournalMetaDataAccessObject(IMongoDatabaseProvider databaseProvider)
        {
            if (databaseProvider == null)
            {
                throw new ArgumentNullException(nameof(databaseProvider));
            }

            this.db = databaseProvider.Create();
            this.CollectionName = MongoCollectionNameFactory.Create<JournalMeta>();
        }

        private string CollectionName { get; set; }

        private IMongoCollection<JournalMeta> Collection => this.db.GetCollection<JournalMeta>(this.CollectionName);

        /// <inheritdoc/>
        public async Task<IJournalMeta[]> GetAllAsync()
        {
            var data = await this.Collection.Find(m => true).ToListAsync().ConfigureAwait(false);
            return data.ToArray<IJournalMeta>();
        }
    }
}
