// <copyright file="MongoStaffDataAccessObject.cs" company="ProcessingTools">
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
    /// MongoDB implementation of <see cref="IStaffDataAccessObject"/>.
    /// </summary>
    public class MongoStaffDataAccessObject : BaseDataAccessObject<IStaff, Staff>, IStaffDataAccessObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MongoStaffDataAccessObject"/> class.
        /// </summary>
        /// <param name="collection">MongoDB collection of <see cref="Staff"/>.</param>
        /// <param name="applicationContext">Instance of <see cref="IApplicationContext"/>.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        public MongoStaffDataAccessObject(IMongoCollection<Staff> collection, IApplicationContext applicationContext, IMapper mapper)
            : base(collection, applicationContext, mapper)
        {
        }

        /// <inheritdoc/>
        public Task<long> SaveChangesAsync() => Task.FromResult(-1L);

        /// <inheritdoc/>
        public Task<object> InsertStaffAsync(IEnumerable<IStaff> staffs)
        {
            if (staffs is null)
            {
                return Task.FromResult(default(object));
            }

            return this.InsertManyAsync(staffs);
        }
    }
}
