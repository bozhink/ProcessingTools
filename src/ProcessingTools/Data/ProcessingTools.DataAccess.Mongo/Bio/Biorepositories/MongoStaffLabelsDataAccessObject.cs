// <copyright file="MongoStaffLabelsDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
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
    /// MongoDB implementation of <see cref="IStaffLabelsDataAccessObject"/>.
    /// </summary>
    public class MongoStaffLabelsDataAccessObject : BaseDataAccessObject<IStaffLabel, StaffLabel>, IStaffLabelsDataAccessObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MongoStaffLabelsDataAccessObject"/> class.
        /// </summary>
        /// <param name="collection">MongoDB collection of <see cref="StaffLabel"/>.</param>
        /// <param name="applicationContext">Instance of <see cref="IApplicationContext"/>.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        public MongoStaffLabelsDataAccessObject(IMongoCollection<StaffLabel> collection, IApplicationContext applicationContext, IMapper mapper)
            : base(collection, applicationContext, mapper)
        {
        }

        /// <inheritdoc/>
        public Task<long> SaveChangesAsync() => Task.FromResult(-1L);

        /// <inheritdoc/>
        public Task<object> InsertStaffLabelsAsync(IEnumerable<IStaffLabel> staffLabels)
        {
            if (staffLabels is null)
            {
                return Task.FromResult(default(object));
            }

            return this.InsertManyAsync(staffLabels);
        }
    }
}
