// <copyright file="MongoCollectionPerLabelsDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
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
    /// MongoDB implementation of <see cref="ICollectionPerLabelsDataAccessObject"/>.
    /// </summary>
    public class MongoCollectionPerLabelsDataAccessObject : BaseDataAccessObject<ICollectionPerLabel, CollectionPerLabel>, ICollectionPerLabelsDataAccessObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MongoCollectionPerLabelsDataAccessObject"/> class.
        /// </summary>
        /// <param name="collection">MongoDB collection of <see cref="CollectionPerLabel"/>.</param>
        /// <param name="applicationContext">Instance of <see cref="IApplicationContext"/>.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        public MongoCollectionPerLabelsDataAccessObject(IMongoCollection<CollectionPerLabel> collection, IApplicationContext applicationContext, IMapper mapper)
            : base(collection, applicationContext, mapper)
        {
        }

        /// <inheritdoc/>
        public Task<long> SaveChangesAsync() => Task.FromResult(-1L);

        /// <inheritdoc/>
        public Task<object> InsertCollectionPerLabelsAsync(IEnumerable<ICollectionPerLabel> collectionPerLabels)
        {
            if (collectionPerLabels is null)
            {
                return Task.FromResult(default(object));
            }

            return this.InsertManyAsync(collectionPerLabels);
        }
    }
}
