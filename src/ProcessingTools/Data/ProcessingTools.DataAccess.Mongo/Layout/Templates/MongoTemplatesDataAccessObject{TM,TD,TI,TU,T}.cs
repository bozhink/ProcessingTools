// <copyright file="MongoTemplatesDataAccessObject{TM,TD,TI,TU,T}.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DataAccess.Mongo.Layout.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using MongoDB.Driver;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts.DataAccess;
    using ProcessingTools.Contracts.DataAccess.Layout.Templates;
    using ProcessingTools.Contracts.DataAccess.Models;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Templates;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Layout.Templates;
    using ProcessingTools.Data.Models.Mongo;
    using ProcessingTools.Extensions;

    /// <summary>
    /// Generic implementation of templates data access object (DAO).
    /// </summary>
    /// <typeparam name="TM">Type of the data transfer object (DTO).</typeparam>
    /// <typeparam name="TD">Type of the detailed data transfer object (DTO).</typeparam>
    /// <typeparam name="TI">Type of the insert model.</typeparam>
    /// <typeparam name="TU">Type of the update model.</typeparam>
    /// <typeparam name="T">Type of the data model.</typeparam>
    public abstract class MongoTemplatesDataAccessObject<TM, TD, TI, TU, T> : ITemplatesDataAccessObject, IDataAccessObject<TM, TD, TI, TU>
        where TM : class, IIdentifiedTemplateDataTransferObject, IDataTransferObject
        where TD : class, IIdentifiedTemplateDataTransferObject, IDataTransferObject
        where TI : class, ITemplateModel
        where TU : class, IIdentifiedTemplateModel
        where T : MongoDataModel, ITemplateModel
    {
        private readonly IMongoCollection<T> collection;
        private readonly IApplicationContext applicationContext;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoTemplatesDataAccessObject{TM,TD,TI,TU,T}"/> class.
        /// </summary>
        /// <param name="collection">Instance of <see cref="IMongoCollection{T}"/>.</param>
        /// <param name="applicationContext">Instance of <see cref="IApplicationContext"/>.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        protected MongoTemplatesDataAccessObject(IMongoCollection<T> collection, IApplicationContext applicationContext, IMapper mapper)
        {
            this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
            this.applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <inheritdoc/>
        public virtual async Task<object> DeleteAsync(object id)
        {
            if (id is null)
            {
                return null;
            }

            Guid objectId = id.ToNewGuid();

            var result = await this.collection.DeleteOneAsync(s => s.ObjectId == objectId).ConfigureAwait(false);

            if (!result.IsAcknowledged)
            {
                throw new DeleteUnsuccessfulException();
            }

            return result;
        }

        /// <inheritdoc/>
        public virtual async Task<TM> GetByIdAsync(object id)
        {
            if (id is null)
            {
                return null;
            }

            Guid objectId = id.ToNewGuid();

            var dbmodel = await this.collection.Find(s => s.ObjectId == objectId).FirstOrDefaultAsync().ConfigureAwait(false);
            if (dbmodel is null)
            {
                return null;
            }

            return this.mapper.Map<TM>(dbmodel);
        }

        /// <inheritdoc/>
        public virtual async Task<TD> GetDetailsByIdAsync(object id)
        {
            if (id is null)
            {
                return null;
            }

            Guid objectId = id.ToNewGuid();

            var dbmodel = await this.collection.Find(s => s.ObjectId == objectId).FirstOrDefaultAsync().ConfigureAwait(false);
            if (dbmodel is null)
            {
                return null;
            }

            return this.mapper.Map<TD>(dbmodel);
        }

        /// <inheritdoc/>
        public virtual async Task<string> GetTemplateContentByIdAsync(object id)
        {
            if (id is null)
            {
                return null;
            }

            Guid objectId = id.ToNewGuid();

            var content = await this.collection.Find(s => s.ObjectId == objectId).Project(t => t.Content).FirstOrDefaultAsync().ConfigureAwait(false);

            return content;
        }

        /// <inheritdoc/>
        public virtual async Task<IList<IIdentifiedTemplateMetaDataTransferObject>> GetTemplatesForSelectAsync()
        {
            var projection = Builders<T>.Projection.Exclude(t => t.Content);

            var data = await this.collection.Find(Builders<T>.Filter.Empty).Project(projection).As<T>()
                .SortBy(p => p.Name)
                .ToListAsync()
                .ConfigureAwait(false);

            if (data == null || !data.Any())
            {
                return Array.Empty<IIdentifiedTemplateMetaDataTransferObject>();
            }

            return data.Select(this.mapper.Map<IIdentifiedTemplateMetaDataTransferObject>).ToArray();
        }

        /// <inheritdoc/>
        public Task<TM> InsertAsync(TI model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return this.InsertInternalAsync(model);
        }

        /// <inheritdoc/>
        public Task<long> SaveChangesAsync() => Task.FromResult(-1L);

        /// <inheritdoc/>
        public virtual async Task<IList<TM>> SelectAsync(int skip, int take)
        {
            var projection = Builders<T>.Projection.Exclude(t => t.Content);

            var data = await this.collection.Find(Builders<T>.Filter.Empty).Project(projection).As<T>()
                .SortBy(p => p.Name)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (data == null || !data.Any())
            {
                return Array.Empty<TM>();
            }

            return data.Select(this.mapper.Map<TM>).ToArray();
        }

        /// <inheritdoc/>
        public virtual Task<long> SelectCountAsync()
        {
            return this.collection.CountDocumentsAsync(Builders<T>.Filter.Empty);
        }

        /// <inheritdoc/>
        public virtual async Task<IList<TD>> SelectDetailsAsync(int skip, int take)
        {
            var data = await this.collection.Find(Builders<T>.Filter.Empty)
                .SortBy(p => p.Name)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (data == null || !data.Any())
            {
                return Array.Empty<TD>();
            }

            return data.Select(this.mapper.Map<TD>).ToArray();
        }

        /// <inheritdoc/>
        public Task<TM> UpdateAsync(TU model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return this.UpdateInternalAsync(model);
        }

        private async Task<TM> InsertInternalAsync(TI model)
        {
            var dbmodel = this.mapper.Map<T>(model);
            dbmodel.ObjectId = this.applicationContext.GuidProvider.Invoke();
            dbmodel.ModifiedBy = this.applicationContext.UserContext.UserId;
            dbmodel.ModifiedOn = this.applicationContext.DateTimeProvider.Invoke();
            dbmodel.CreatedBy = dbmodel.ModifiedBy;
            dbmodel.CreatedOn = dbmodel.ModifiedOn;
            dbmodel.Id = null;

            await this.collection.InsertOneAsync(dbmodel, new InsertOneOptions { BypassDocumentValidation = false }).ConfigureAwait(false);

            return this.mapper.Map<TM>(dbmodel);
        }

        private async Task<TM> UpdateInternalAsync(TU model)
        {
            Guid objectId = model.Id.ToNewGuid();

            var dbmodel = this.mapper.Map<T>(model);
            dbmodel.ModifiedBy = this.applicationContext.UserContext.UserId;
            dbmodel.ModifiedOn = this.applicationContext.DateTimeProvider.Invoke();

            var filterDefinition = new FilterDefinitionBuilder<T>().Eq(m => m.ObjectId, objectId);
            var updateDefinition = new UpdateDefinitionBuilder<T>()
                .Set(s => s.Name, model.Name)
                .Set(s => s.Description, model.Description)
                .Set(s => s.Content, model.Content)
                .Set(s => s.ModifiedBy, dbmodel.ModifiedBy)
                .Set(s => s.ModifiedOn, dbmodel.ModifiedOn);
            var updateOptions = new UpdateOptions
            {
                BypassDocumentValidation = false,
                IsUpsert = false,
            };

            var result = await this.collection.UpdateOneAsync(filterDefinition, updateDefinition, updateOptions).ConfigureAwait(false);

            if (!result.IsAcknowledged)
            {
                throw new UpdateUnsuccessfulException();
            }

            return this.mapper.Map<TM>(dbmodel);
        }
    }
}
