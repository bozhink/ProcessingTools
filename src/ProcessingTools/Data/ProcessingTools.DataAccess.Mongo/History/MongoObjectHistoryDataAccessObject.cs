// <copyright file="MongoObjectHistoryDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DataAccess.Mongo.History
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using MongoDB.Driver;
    using ProcessingTools.Contracts.DataAccess.History;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.History;
    using ProcessingTools.Data.Models.Mongo.History;

    /// <summary>
    /// MongoDB implementation of <see cref="IObjectHistoryDataAccessObject"/>.
    /// </summary>
    public class MongoObjectHistoryDataAccessObject : IObjectHistoryDataAccessObject
    {
        private readonly IMongoCollection<ObjectHistory> collection;
        private readonly IApplicationContext applicationContext;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoObjectHistoryDataAccessObject"/> class.
        /// </summary>
        /// <param name="collection">Instance of <see cref="IMongoCollection{ObjectHistory}"/>.</param>
        /// <param name="applicationContext">Application context.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        public MongoObjectHistoryDataAccessObject(IMongoCollection<ObjectHistory> collection, IApplicationContext applicationContext, IMapper mapper)
        {
            this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
            this.applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <inheritdoc/>
        public async Task<IObjectHistory> AddAsync(IObjectHistory objectHistory)
        {
            if (objectHistory == null)
            {
                return null;
            }

            var item = this.mapper.Map<IObjectHistory, ObjectHistory>(objectHistory);
            item.CreatedBy = this.applicationContext.UserContext.UserId;
            item.CreatedOn = this.applicationContext.DateTimeProvider.Invoke();
            item.Id = null;

            await this.collection.InsertOneAsync(item, new InsertOneOptions { BypassDocumentValidation = true }).ConfigureAwait(false);

            return item;
        }

        /// <inheritdoc/>
        public async Task<IList<IObjectHistory>> GetAsync(object objectId)
        {
            if (objectId == null)
            {
                return Array.Empty<IObjectHistory>();
            }

            var data = await this.collection
                .Find(h => h.ObjectId == objectId.ToString())
                .Sort(Builders<ObjectHistory>.Sort.Descending(h => h.CreatedOn))
                .ToListAsync()
                .ConfigureAwait(false);

            if (data == null)
            {
                return Array.Empty<IObjectHistory>();
            }

            return data.ToArray<IObjectHistory>();
        }

        /// <inheritdoc/>
        public async Task<IList<IObjectHistory>> GetAsync(object objectId, int skip, int take)
        {
            if (objectId == null)
            {
                return Array.Empty<IObjectHistory>();
            }

            var data = await this.collection
                .Find(h => h.ObjectId == objectId.ToString())
                .Sort(Builders<ObjectHistory>.Sort.Descending(h => h.CreatedOn))
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (data == null)
            {
                return Array.Empty<IObjectHistory>();
            }

            return data.ToArray<IObjectHistory>();
        }

        /// <inheritdoc/>
        public Task<long> SaveChangesAsync() => Task.FromResult(-1L);
    }
}
