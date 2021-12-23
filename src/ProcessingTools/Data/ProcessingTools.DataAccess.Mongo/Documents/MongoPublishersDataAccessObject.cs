// <copyright file="MongoPublishersDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DataAccess.Mongo.Documents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using MongoDB.Driver;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Common.Resources;
    using ProcessingTools.Contracts.DataAccess.Documents;
    using ProcessingTools.Contracts.DataAccess.Models.Documents.Publishers;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Documents.Publishers;
    using ProcessingTools.Data.Models.Mongo.Documents;
    using ProcessingTools.Data.Mongo;
    using ProcessingTools.Data.Mongo.Abstractions;
    using ProcessingTools.Extensions;

    /// <summary>
    /// MongoDB implementation of <see cref="IPublishersDataAccessObject"/>.
    /// </summary>
    public class MongoPublishersDataAccessObject : MongoDataAccessObjectBase<Publisher>, IPublishersDataAccessObject
    {
        private readonly IApplicationContext applicationContext;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoPublishersDataAccessObject"/> class.
        /// </summary>
        /// <param name="databaseProvider">Instance of <see cref="IMongoDatabaseProvider"/>.</param>
        /// <param name="applicationContext">Application context.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        public MongoPublishersDataAccessObject(IMongoDatabaseProvider databaseProvider, IApplicationContext applicationContext, IMapper mapper)
            : base(databaseProvider)
        {
            this.applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            this.CollectionSettings = new MongoCollectionSettings
            {
                AssignIdOnInsert = true,
                WriteConcern = new WriteConcern(WriteConcern.WMajority.W),
            };
        }

        /// <inheritdoc/>
        public async Task<IPublisherDataTransferObject> GetByIdAsync(object id) => await this.GetDetailsByIdAsync(id).ConfigureAwait(false);

        /// <inheritdoc/>
        public async Task<IPublisherDetailsDataTransferObject> GetDetailsByIdAsync(object id)
        {
            if (id is null)
            {
                return null;
            }

            Guid objectId = id.ToNewGuid();

            var publisher = await this.Collection.Find(p => p.ObjectId == objectId).FirstOrDefaultAsync().ConfigureAwait(false);

            if (publisher != null)
            {
                var numberOfJournals = await this.GetCollection<Journal>().CountDocumentsAsync(j => j.PublisherId == publisher.ObjectId.ToString()).ConfigureAwait(false);

                publisher.NumberOfJournals = numberOfJournals;
            }

            return publisher;
        }

        /// <inheritdoc/>
        public async Task<object> DeleteAsync(object id)
        {
            if (id is null)
            {
                return null;
            }

            long numberOfJournals = await this.GetCollection<Journal>().CountDocumentsAsync(j => j.PublisherId == id.ToString()).ConfigureAwait(false);
            if (numberOfJournals > 0L)
            {
                throw new DeleteUnsuccessfulException(StringResources.PublisherWillNotBeDeletedBecauseItContainsRelatedJournals);
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
        public async Task<IPublisherDataTransferObject> InsertAsync(IPublisherInsertModel model)
        {
            if (model is null)
            {
                return null;
            }

            var publisher = this.mapper.Map<IPublisherInsertModel, Publisher>(model);
            publisher.ObjectId = this.applicationContext.GuidProvider.Invoke();
            publisher.ModifiedBy = this.applicationContext.UserContext.UserId;
            publisher.ModifiedOn = this.applicationContext.DateTimeProvider.Invoke();
            publisher.CreatedBy = publisher.ModifiedBy;
            publisher.CreatedOn = publisher.ModifiedOn;
            publisher.Id = null;

            await this.Collection.InsertOneAsync(publisher, new InsertOneOptions { BypassDocumentValidation = false }).ConfigureAwait(false);

            return publisher;
        }

        /// <inheritdoc/>
        public async Task<IList<IPublisherDataTransferObject>> SelectAsync(int skip, int take)
        {
            var publishers = await this.Collection.Find(Builders<Publisher>.Filter.Empty)
                .SortBy(p => p.Name)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (publishers is null || !publishers.Any())
            {
                return Array.Empty<IPublisherDataTransferObject>();
            }

            return publishers.ToArray<IPublisherDataTransferObject>();
        }

        /// <inheritdoc/>
        public async Task<IList<IPublisherDetailsDataTransferObject>> SelectDetailsAsync(int skip, int take)
        {
            var publishers = await this.Collection.Find(Builders<Publisher>.Filter.Empty)
                .SortBy(p => p.Name)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (publishers is null || !publishers.Any())
            {
                return Array.Empty<IPublisherDetailsDataTransferObject>();
            }

            var journals = this.GetCollection<Journal>().AsQueryable()
                .GroupBy(j => j.PublisherId)
                .Select(g => new { PublisherId = g.Key, Count = g.LongCount() })
                .ToArray();

            if (journals != null && journals.Any())
            {
                publishers.ForEach(p =>
                {
                    p.NumberOfJournals = journals.FirstOrDefault(j => j.PublisherId == p.ObjectId.ToString())?.Count ?? 0L;
                });
            }

            return publishers.ToArray<IPublisherDetailsDataTransferObject>();
        }

        /// <inheritdoc/>
        public Task<long> SelectCountAsync()
        {
            return this.Collection.CountDocumentsAsync(Builders<Publisher>.Filter.Empty);
        }

        /// <inheritdoc/>
        public async Task<IPublisherDataTransferObject> UpdateAsync(IPublisherUpdateModel model)
        {
            if (model is null)
            {
                return null;
            }

            Guid objectId = model.Id.ToNewGuid();

            var publisher = this.mapper.Map<IPublisherUpdateModel, Publisher>(model);
            publisher.ModifiedBy = this.applicationContext.UserContext.UserId;
            publisher.ModifiedOn = this.applicationContext.DateTimeProvider.Invoke();

            var filterDefinition = new FilterDefinitionBuilder<Publisher>().Eq(m => m.ObjectId, objectId);
            var updateDefinition = new UpdateDefinitionBuilder<Publisher>()
                .Set(p => p.AbbreviatedName, model.AbbreviatedName)
                .Set(p => p.Name, model.Name)
                .Set(p => p.Address, model.Address)
                .Set(p => p.ModifiedBy, publisher.ModifiedBy)
                .Set(p => p.ModifiedOn, publisher.ModifiedOn);
            var updateOptions = new UpdateOptions
            {
                BypassDocumentValidation = false,
                IsUpsert = false,
            };

            var result = await this.Collection.UpdateOneAsync(filterDefinition, updateDefinition, updateOptions).ConfigureAwait(false);

            if (!result.IsAcknowledged)
            {
                throw new UpdateUnsuccessfulException();
            }

            return publisher;
        }
    }
}
