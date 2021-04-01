// <copyright file="MongoJournalMetaDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DataAccess.Mongo.Documents
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using MongoDB.Driver;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts.DataAccess.Documents;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Documents;
    using ProcessingTools.Data.Models.Mongo.Documents;
    using ProcessingTools.Data.Mongo;
    using ProcessingTools.Data.Mongo.Abstractions;

    /// <summary>
    /// MongoDB implementation of Journals meta data access object.
    /// </summary>
    public class MongoJournalMetaDataAccessObject : MongoDataAccessObjectBase<JournalMeta>, IJournalMetaDataAccessObject
    {
        private readonly IApplicationContext applicationContext;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoJournalMetaDataAccessObject"/> class.
        /// </summary>
        /// <param name="databaseProvider">Instance of <see cref="IMongoDatabaseProvider"/>.</param>
        /// <param name="applicationContext">Application context.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        public MongoJournalMetaDataAccessObject(IMongoDatabaseProvider databaseProvider, IApplicationContext applicationContext, IMapper mapper)
            : base(databaseProvider)
        {
            this.applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <inheritdoc/>
        public async Task<object> DeleteAsync(object id)
        {
            if (id is null)
            {
                return null;
            }

            var result = await this.Collection.DeleteOneAsync(m => m.Id == id.ToString()).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public async Task<IJournalMeta[]> GetAllAsync()
        {
            var data = await this.Collection.Find(m => true).ToListAsync().ConfigureAwait(false);
            return data.ToArray<IJournalMeta>();
        }

        /// <inheritdoc/>
        public async Task<IJournalMeta> GetAsync(object id)
        {
            if (id is null)
            {
                return null;
            }

            var item = await this.Collection.Find(m => m.Id == id.ToString()).FirstOrDefaultAsync().ConfigureAwait(false);
            return item;
        }

        /// <inheritdoc/>
        public async Task<IJournalMeta> InsertAsync(IJournalMeta journalMeta)
        {
            if (journalMeta is null)
            {
                return null;
            }

            var item = this.mapper.Map<IJournalMeta, JournalMeta>(journalMeta);
            item.ModifiedBy = this.applicationContext.UserContext.UserId;
            item.ModifiedOn = this.applicationContext.DateTimeProvider.Invoke();
            item.CreatedBy = item.ModifiedBy;
            item.CreatedOn = item.ModifiedOn;
            item.Id = null;

            await this.Collection.InsertOneAsync(item).ConfigureAwait(false);

            return item;
        }

        /// <inheritdoc/>
        public async Task<IJournalMeta> UpdateAsync(IJournalMeta journalMeta)
        {
            if (journalMeta is null)
            {
                return null;
            }

            var item = this.mapper.Map<IJournalMeta, JournalMeta>(journalMeta);
            item.ModifiedBy = this.applicationContext.UserContext.UserId;
            item.ModifiedOn = this.applicationContext.DateTimeProvider.Invoke();

            var filterDefinition = new FilterDefinitionBuilder<JournalMeta>().Eq(m => m.Id, item.Id);
            var updateDefinition = new UpdateDefinitionBuilder<JournalMeta>()
                .Set(m => m.AbbreviatedJournalTitle, item.AbbreviatedJournalTitle)
                .Set(m => m.ArchiveNamePattern, item.ArchiveNamePattern)
                .Set(m => m.FileNamePattern, item.FileNamePattern)
                .Set(m => m.IssnEPub, item.IssnEPub)
                .Set(m => m.IssnPPub, item.IssnPPub)
                .Set(m => m.JournalId, item.JournalId)
                .Set(m => m.JournalTitle, item.JournalTitle)
                .Set(m => m.PublisherName, item.PublisherName)
                .Set(m => m.AbbreviatedJournalTitle, item.AbbreviatedJournalTitle)
                .Set(m => m.ModifiedBy, item.ModifiedBy)
                .Set(m => m.ModifiedOn, item.ModifiedOn);
            var updateOptions = new UpdateOptions
            {
                BypassDocumentValidation = false,
                IsUpsert = false,
            };

            var result = await this.Collection.UpdateOneAsync(filterDefinition, updateDefinition, updateOptions).ConfigureAwait(false);

            if (result.UpsertedId.AsString != journalMeta.Id)
            {
                throw new UpdateUnsuccessfulException();
            }

            return item;
        }
    }
}
