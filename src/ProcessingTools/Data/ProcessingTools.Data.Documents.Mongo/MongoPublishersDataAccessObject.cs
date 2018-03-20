// <copyright file="MongoPublishersDataAccessObject.cs" company="ProcessingTools">
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
    using ProcessingTools.Data.Models.Contracts.Documents.Publishers;
    using ProcessingTools.Data.Models.Documents.Mongo;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Extensions;
    using ProcessingTools.Models.Contracts.Documents.Publishers;

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
        public MongoPublishersDataAccessObject(IMongoDatabaseProvider databaseProvider, IApplicationContext applicationContext)
            : base(databaseProvider)
        {
            this.applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IPublisherInsertModel, Publisher>();
                c.CreateMap<IPublisherUpdateModel, Publisher>();
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
        public async Task<IPublisherDataModel> GetById(object id) => await this.GetDetailsById(id).ConfigureAwait(false);

        /// <inheritdoc/>
        public async Task<IPublisherDetailsDataModel> GetDetailsById(object id)
        {
            if (id == null)
            {
                return null;
            }

            Guid objectId = id.ToNewGuid();

            var publisher = await this.Collection.Find(p => p.ObjectId == objectId).FirstOrDefaultAsync().ConfigureAwait(false);

            return publisher;
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
        public async Task<IPublisherDataModel> InsertAsync(IPublisherInsertModel model)
        {
            if (model == null)
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

            await this.Collection.InsertOneAsync(publisher).ConfigureAwait(false);

            return publisher;
        }

        /// <inheritdoc/>
        public async Task<IPublisherDataModel[]> SelectAsync(int skip, int take)
        {
            var publishers = await this.Collection.Find(p => true)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (publishers == null || !publishers.Any())
            {
                return new IPublisherDataModel[] { };
            }

            return publishers.ToArray<IPublisherDataModel>();
        }

        /// <inheritdoc/>
        public async Task<IPublisherDetailsDataModel[]> SelectDetailsAsync(int skip, int take)
        {
            var publishers = await this.Collection.Find(p => true)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (publishers == null || !publishers.Any())
            {
                return new IPublisherDetailsDataModel[] { };
            }

            return publishers.ToArray<IPublisherDetailsDataModel>();
        }

        /// <inheritdoc/>
        public Task<long> SelectCountAsync()
        {
            return this.Collection.CountAsync(p => true);
        }

        /// <inheritdoc/>
        public async Task<IPublisherDataModel> UpdateAsync(IPublisherUpdateModel model)
        {
            if (model == null)
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
                IsUpsert = false
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
