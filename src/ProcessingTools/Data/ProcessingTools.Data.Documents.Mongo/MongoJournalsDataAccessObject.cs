// <copyright file="MongoJournalsDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Documents.Mongo
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using MongoDB.Driver;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Common.Mongo;
    using ProcessingTools.Data.Common.Mongo.Contracts;
    using ProcessingTools.Data.Contracts.Documents;
    using ProcessingTools.Data.Models.Contracts.Documents.Journals;
    using ProcessingTools.Data.Models.Documents.Mongo;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Extensions;
    using ProcessingTools.Models.Contracts.Documents.Journals;

    /// <summary>
    /// MongoDB implementation of <see cref="IJournalsDataAccessObject"/>.
    /// </summary>
    public class MongoJournalsDataAccessObject : MongoDataAccessObjectBase<Journal>, IJournalsDataAccessObject
    {
        private readonly IMapper mapper;
        private readonly IApplicationContext applicationContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoJournalsDataAccessObject"/> class.
        /// </summary>
        /// <param name="databaseProvider">Instance of <see cref="IMongoDatabaseProvider"/>.</param>
        /// <param name="applicationContext">Application context.</param>
        public MongoJournalsDataAccessObject(IMongoDatabaseProvider databaseProvider, IApplicationContext applicationContext)
            : base(databaseProvider)
        {
            this.applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IJournalInsertModel, Journal>();
                c.CreateMap<IJournalUpdateModel, Journal>();
            });

            this.mapper = mapperConfiguration.CreateMapper();

            this.CollectionSettings = new MongoCollectionSettings
            {
                AssignIdOnInsert = true,
                GuidRepresentation = MongoDB.Bson.GuidRepresentation.Standard,
                WriteConcern = new WriteConcern(WriteConcern.WMajority.W)
            };
        }

        /// <inheritdoc/>
        public async Task<object> DeleteAsync(object id)
        {
            if (id == null)
            {
                return null;
            }

            Guid objectId = id.ToNewGuid();

            var result = await this.Collection.DeleteOneAsync(p => p.ObjectId == objectId).ConfigureAwait(false);

            if (!result.IsAcknowledged)
            {
                throw new DeleteUnsuccessfulException();
            }

            return result;
        }

        /// <inheritdoc/>
        public async Task<IJournalDataModel> GetById(object id) => await this.GetDetailsById(id).ConfigureAwait(false);

        /// <inheritdoc/>
        public async Task<IJournalDetailsDataModel> GetDetailsById(object id)
        {
            if (id == null)
            {
                return null;
            }

            Guid objectId = id.ToNewGuid();

            var item = await this.Collection.Find(p => p.ObjectId == objectId).FirstOrDefaultAsync().ConfigureAwait(false);

            return item;
        }

        /// <inheritdoc/>
        public async Task<IJournalPublisherDataModel[]> GetJournalPublishersAsync()
        {
            var collection = this.GetCollection<Publisher>();

            var data = await collection.Find(p => true).
                Project(p => new JournalPublisher
                {
                    Id = p.Id,
                    ObjectId = p.ObjectId,
                    Name = p.Name,
                    AbbreviatedName = p.AbbreviatedName
                })
                .ToListAsync()
                .ConfigureAwait(false);

            return data.ToArray<IJournalPublisherDataModel>();
        }

        /// <inheritdoc/>
        public async Task<IJournalDataModel> InsertAsync(IJournalInsertModel model)
        {
            if (model == null)
            {
                return null;
            }

            var item = this.mapper.Map<IJournalInsertModel, Journal>(model);
            item.ObjectId = this.applicationContext.GuidProvider.Invoke();
            item.ModifiedBy = this.applicationContext.UserContext.UserId;
            item.ModifiedOn = this.applicationContext.DateTimeProvider.Invoke();
            item.CreatedBy = item.ModifiedBy;
            item.CreatedOn = item.ModifiedOn;
            item.Id = null;

            await this.Collection.InsertOneAsync(item).ConfigureAwait(false);

            return item;
        }

        /// <inheritdoc/>
        public async Task<IJournalDataModel[]> SelectAsync(int skip, int take)
        {
            var data = await this.Collection.Find(p => true).ToListAsync().ConfigureAwait(false);
            if (data == null || !data.Any())
            {
                return new IJournalDataModel[] { };
            }

            return data.ToArray<IJournalDataModel>();
        }

        /// <inheritdoc/>
        public Task<long> SelectCountAsync()
        {
            return this.Collection.CountAsync(p => true);
        }

        /// <inheritdoc/>
        public async Task<IJournalDataModel> UpdateAsync(IJournalUpdateModel model)
        {
            if (model == null)
            {
                return null;
            }

            Guid objectId = model.Id.ToNewGuid();

            var item = this.mapper.Map<IJournalUpdateModel, Journal>(model);
            item.ModifiedBy = this.applicationContext.UserContext.UserId;
            item.ModifiedOn = this.applicationContext.DateTimeProvider.Invoke();

            var filterDefinition = new FilterDefinitionBuilder<Journal>().Eq(m => m.ObjectId, objectId);
            var updateDefinition = new UpdateDefinitionBuilder<Journal>()
                .Set(p => p.Name, model.Name)
                .Set(p => p.AbbreviatedName, model.AbbreviatedName)
                .Set(p => p.JournalId, model.JournalId)
                .Set(p => p.PrintIssn, model.PrintIssn)
                .Set(p => p.ElectronicIssn, model.ElectronicIssn)
                .Set(p => p.PublisherId, model.PublisherId)
                .Set(p => p.ModifiedBy, item.ModifiedBy)
                .Set(p => p.ModifiedOn, item.ModifiedOn);
            var updateOptions = new UpdateOptions
            {
                BypassDocumentValidation = false,
                IsUpsert = false
            };

            var result = await this.Collection.UpdateOneAsync(filterDefinition, updateDefinition, updateOptions).ConfigureAwait(false);

            if (!result.IsAcknowledged)
            {
                throw new UpdateUnsuccessfulException();
            }

            return item;
        }
    }
}
