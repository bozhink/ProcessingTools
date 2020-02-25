// <copyright file="BaseDataAccessObject{TM,TD}.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DataAccess.Mongo.Bio.Biorepositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using MongoDB.Driver;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Data.Models.Mongo;

    /// <summary>
    /// Biorepository base data access object (DAO).
    /// </summary>
    /// <typeparam name="TM">Type of the model.</typeparam>
    /// <typeparam name="TD">Type of the document.</typeparam>
    public abstract class BaseDataAccessObject<TM, TD>
        where TM : class
        where TD : MongoDataModel, new()
    {
        private readonly IMongoCollection<TD> collection;
        private readonly IApplicationContext applicationContext;
        private readonly IMapper mapper;

        private readonly InsertManyOptions insertManyOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDataAccessObject{TM, TD}"/> class.
        /// </summary>
        /// <param name="collection">MongoDB collection of the document type.</param>
        /// <param name="applicationContext">Instance of <see cref="IApplicationContext"/>.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        protected BaseDataAccessObject(IMongoCollection<TD> collection, IApplicationContext applicationContext, IMapper mapper)
        {
            this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
            this.applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            this.insertManyOptions = new InsertManyOptions
            {
                BypassDocumentValidation = false,
                IsOrdered = false,
            };
        }

        /// <summary>
        /// Insert list of items.
        /// </summary>
        /// <param name="items">Items to be inserted.</param>
        /// <returns>Null if items is null.</returns>
        protected async Task<object> InsertManyAsync(IEnumerable<TM> items)
        {
            if (items is null)
            {
                return null;
            }

            var documents = items.Select(this.MapToDocument).ToList();

            await this.collection.InsertManyAsync(documents, this.insertManyOptions).ConfigureAwait(false);

            return true;
        }

        private TD MapToDocument(TM item)
        {
            TD document = this.mapper.Map<TM, TD>(item);

            document.ObjectId = this.applicationContext.GuidProvider.Invoke();
            document.ModifiedBy = this.applicationContext.UserContext.UserId;
            document.ModifiedOn = this.applicationContext.DateTimeProvider.Invoke();
            document.CreatedBy = document.ModifiedBy;
            document.CreatedOn = document.ModifiedOn;
            document.Id = null;

            return document;
        }
    }
}
