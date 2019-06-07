﻿// <copyright file="MongoObjectHistoryDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Mongo.History
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using MongoDB.Driver;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.DataAccess.History;
    using ProcessingTools.Contracts.Models.History;
    using ProcessingTools.Data.Models.Mongo.History;
    using ProcessingTools.Data.Mongo.Abstractions;

    /// <summary>
    /// MongoDB implementation of <see cref="IObjectHistoryDataAccessObject"/>.
    /// </summary>
    public class MongoObjectHistoryDataAccessObject : MongoDataAccessObjectBase<ObjectHistory>, IObjectHistoryDataAccessObject
    {
        private readonly IMapper mapper;
        private readonly IApplicationContext applicationContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoObjectHistoryDataAccessObject"/> class.
        /// </summary>
        /// <param name="databaseProvider">Instance of <see cref="IMongoDatabaseProvider"/>.</param>
        /// <param name="applicationContext">Application context.</param>
        public MongoObjectHistoryDataAccessObject(IMongoDatabaseProvider databaseProvider, IApplicationContext applicationContext)
            : base(databaseProvider)
        {
            this.applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IObjectHistory, ObjectHistory>();
            });

            this.mapper = mapperConfiguration.CreateMapper();

            this.CollectionSettings = new MongoCollectionSettings
            {
                AssignIdOnInsert = true,
                GuidRepresentation = MongoDB.Bson.GuidRepresentation.Standard,
                ReadPreference = new ReadPreference(ReadPreferenceMode.SecondaryPreferred),
                WriteConcern = new WriteConcern(WriteConcern.Unacknowledged.W),
            };
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

            await this.Collection.InsertOneAsync(item, new InsertOneOptions { BypassDocumentValidation = true }).ConfigureAwait(false);

            return item;
        }

        /// <inheritdoc/>
        public async Task<IObjectHistory[]> GetAsync(object objectId)
        {
            if (objectId == null)
            {
                return Array.Empty<IObjectHistory>();
            }

            var data = await this.Collection
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
        public async Task<IObjectHistory[]> GetAsync(object objectId, int skip, int take)
        {
            if (objectId == null)
            {
                return Array.Empty<IObjectHistory>();
            }

            var data = await this.Collection
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
    }
}
