// <copyright file="MongoObjectHistoryDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.History.Mongo
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using MongoDB.Driver;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Common.Mongo;
    using ProcessingTools.Data.Common.Mongo.Contracts;
    using ProcessingTools.Data.Contracts.History;
    using ProcessingTools.Data.Models.History.Mongo;
    using ProcessingTools.Models.Contracts.History;

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
        }

        /// <inheritdoc/>
        public async Task<IObjectHistory> AddAsync(IObjectHistory objectHistory)
        {
            if (objectHistory == null)
            {
                throw new ArgumentNullException(nameof(objectHistory));
            }

            var item = this.mapper.Map<IObjectHistory, ObjectHistory>(objectHistory);
            item.CreatedBy = this.applicationContext.UserContext.UserId;
            item.CreatedOn = this.applicationContext.DateTimeProvider.Invoke();

            await this.Collection.InsertOneAsync(item, new InsertOneOptions { BypassDocumentValidation = true }).ConfigureAwait(false);

            return item;
        }

        /// <inheritdoc/>
        public async Task<IObjectHistory[]> GetAsync(object objectId)
        {
            if (objectId == null)
            {
                throw new ArgumentNullException(nameof(objectId));
            }

            var data = await this.Collection
                .Find(h => h.ObjectId == objectId.ToString())
                .ToListAsync()
                .ConfigureAwait(false);

            if (data == null)
            {
                return new IObjectHistory[] { };
            }

            return data.ToArray<IObjectHistory>();
        }

        /// <inheritdoc/>
        public async Task<IObjectHistory[]> GetAsync(object objectId, int skip, int take)
        {
            if (objectId == null)
            {
                throw new ArgumentNullException(nameof(objectId));
            }

            var data = await this.Collection
                .Find(h => h.ObjectId == objectId.ToString())
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (data == null)
            {
                return new IObjectHistory[] { };
            }

            return data.ToArray<IObjectHistory>();
        }
    }
}
