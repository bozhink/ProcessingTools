// <copyright file="MongoCollectionLabelsDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DataAccess.Mongo.Bio.Biorepositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using MongoDB.Driver;
    using ProcessingTools.Contracts.DataAccess.Bio.Biorepositories;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Bio.Biorepositories;
    using ProcessingTools.Data.Models.Mongo.Bio.Biorepositories;

    /// <summary>
    /// MongoDB implementation of <see cref="ICollectionLabelsDataAccessObject"/>.
    /// </summary>
    public class MongoCollectionLabelsDataAccessObject : BaseDataAccessObject<ICollectionLabel, CollectionLabel>, ICollectionLabelsDataAccessObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MongoCollectionLabelsDataAccessObject"/> class.
        /// </summary>
        /// <param name="collection">MongoDB collection of <see cref="CollectionLabel"/>.</param>
        /// <param name="applicationContext">Instance of <see cref="IApplicationContext"/>.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        public MongoCollectionLabelsDataAccessObject(IMongoCollection<CollectionLabel> collection, IApplicationContext applicationContext, IMapper mapper)
            : base(collection, applicationContext, mapper)
        {
        }

        /// <inheritdoc/>
        public Task<long> SaveChangesAsync() => Task.FromResult(-1L);

        /// <inheritdoc/>
        public Task<object> InsertCollectionLabelsAsync(IEnumerable<ICollectionLabel> collectionLabels)
        {
            if (collectionLabels is null)
            {
                return Task.FromResult(default(object));
            }

            return this.InsertManyAsync(collectionLabels);
        }
    }
}
