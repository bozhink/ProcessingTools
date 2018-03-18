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
        public Task<IJournalPublisherDataModel[]> GetJournalPublishersAsync()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<IJournalDataModel> InsertAsync(IJournalInsertModel model)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<IJournalDataModel[]> SelectAsync(int skip, int take)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<long> SelectCountAsync()
        {
            return this.Collection.CountAsync(p => true);
        }

        /// <inheritdoc/>
        public Task<IJournalDataModel> UpdateAsync(IJournalUpdateModel model)
        {
            throw new NotImplementedException();
        }
    }
}
