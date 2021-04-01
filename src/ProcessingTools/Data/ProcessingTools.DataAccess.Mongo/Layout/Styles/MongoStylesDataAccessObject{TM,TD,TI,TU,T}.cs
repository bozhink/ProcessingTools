// <copyright file="MongoStylesDataAccessObject{TM,TD,TI,TU,T}.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DataAccess.Mongo.Layout.Styles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using MongoDB.Driver;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts.DataAccess;
    using ProcessingTools.Contracts.DataAccess.Layout.Styles;
    using ProcessingTools.Contracts.DataAccess.Models;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Layout.Styles;
    using ProcessingTools.Data.Models.Mongo;
    using ProcessingTools.DataAccess.Models.Mongo.Layout.Styles;
    using ProcessingTools.Extensions;

    /// <summary>
    /// Generic implementation of styles data access object (DAO).
    /// </summary>
    /// <typeparam name="TM">Type of the data transfer object (DTO).</typeparam>
    /// <typeparam name="TD">Type of the detailed data transfer object (DTO).</typeparam>
    /// <typeparam name="TI">Type of the insert model.</typeparam>
    /// <typeparam name="TU">Type of the update model.</typeparam>
    /// <typeparam name="T">Type of the data model.</typeparam>
    public abstract class MongoStylesDataAccessObject<TM, TD, TI, TU, T> : IStylesDataAccessObject, IDataAccessObject<TM, TD, TI, TU>
        where TM : class, IIdentifiedStyleDataTransferObject, IDataTransferObject
        where TD : class, IIdentifiedStyleDataTransferObject, IDataTransferObject
        where TI : class, IStyleModel
        where TU : class, IIdentifiedStyleModel
        where T : MongoDataModel, IStyleModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MongoStylesDataAccessObject{TM,TD,TI,TU,T}"/> class.
        /// </summary>
        /// <param name="collection">Instance of <see cref="IMongoCollection{T}"/>.</param>
        /// <param name="applicationContext">Instance of <see cref="IApplicationContext"/>.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        protected MongoStylesDataAccessObject(IMongoCollection<T> collection, IApplicationContext applicationContext, IMapper mapper)
        {
            this.Collection = collection ?? throw new ArgumentNullException(nameof(collection));
            this.ApplicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
            this.Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Gets the instance of the <see cref="IMongoCollection{T}"/>.
        /// </summary>
        protected IMongoCollection<T> Collection { get; }

        /// <summary>
        /// Gets the instance of the <see cref="IApplicationContext"/>.
        /// </summary>
        protected IApplicationContext ApplicationContext { get; }

        /// <summary>
        /// Gets the instance of the <see cref="IMapper"/>.
        /// </summary>
        protected IMapper Mapper { get; }

        /// <inheritdoc/>
        public virtual async Task<object> DeleteAsync(object id)
        {
            if (id is null)
            {
                return null;
            }

            Guid objectId = id.ToNewGuid();

            var result = await this.Collection.DeleteOneAsync(s => s.ObjectId == objectId).ConfigureAwait(false);

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

            var dbmodel = await this.Collection.Find(s => s.ObjectId == objectId).FirstOrDefaultAsync().ConfigureAwait(false);
            if (dbmodel is null)
            {
                return null;
            }

            return this.Mapper.Map<TM>(dbmodel);
        }

        /// <inheritdoc/>
        public virtual async Task<TD> GetDetailsByIdAsync(object id)
        {
            if (id is null)
            {
                return null;
            }

            Guid objectId = id.ToNewGuid();

            var dbmodel = await this.Collection.Find(s => s.ObjectId == objectId).FirstOrDefaultAsync().ConfigureAwait(false);
            if (dbmodel is null)
            {
                return null;
            }

            return this.Mapper.Map<TD>(dbmodel);
        }

        /// <inheritdoc/>
        public async Task<IIdentifiedStyleDataTransferObject> GetStyleByIdAsync(object id)
        {
            if (id is null)
            {
                return null;
            }

            Guid objectId = id.ToNewGuid();

            var style = await this.Collection.Find(s => s.ObjectId == objectId)
                .Project(s => new StyleDataTransferObject
                {
                    Id = s.Id,
                    ObjectId = s.ObjectId,
                    Name = s.Name,
                    Description = s.Description,
                })
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            return style;
        }

        /// <inheritdoc/>
        public async Task<IList<IIdentifiedStyleDataTransferObject>> GetStylesForSelectAsync()
        {
            var data = await this.Collection.Find(Builders<T>.Filter.Empty)
               .Project(s => new StyleDataTransferObject
               {
                   Id = s.Id,
                   ObjectId = s.ObjectId,
                   Name = s.Name,
                   Description = s.Description,
               })
               .ToListAsync()
               .ConfigureAwait(false);

            return data.ToArray();
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
            var dtoProperties = new HashSet<string>(typeof(TM).GetProperties().Select(p => p.Name));
            var modelProperties = new HashSet<string>(typeof(T).GetProperties().Select(p => p.Name));

            ICollection<string> propertiesToExclude = modelProperties.Where(p => !dtoProperties.Contains(p)).ToList();

            ProjectionDefinition<T> projection = null;

            foreach (var item in propertiesToExclude)
            {
                if (projection is null)
                {
                    projection = Builders<T>.Projection.Exclude(item);
                }
                else
                {
                    projection = projection.Exclude(item);
                }
            }

            var data = await this.Collection.Find(Builders<T>.Filter.Empty).Project(projection).As<T>()
                .SortBy(p => p.Name)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (data is null || !data.Any())
            {
                return Array.Empty<TM>();
            }

            return data.Select(this.Mapper.Map<TM>).ToArray();
        }

        /// <inheritdoc/>
        public virtual Task<long> SelectCountAsync()
        {
            return this.Collection.CountDocumentsAsync(Builders<T>.Filter.Empty);
        }

        /// <inheritdoc/>
        public virtual async Task<IList<TD>> SelectDetailsAsync(int skip, int take)
        {
            var data = await this.Collection.Find(Builders<T>.Filter.Empty)
                .SortBy(p => p.Name)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (data is null || !data.Any())
            {
                return Array.Empty<TD>();
            }

            return data.Select(this.Mapper.Map<TD>).ToArray();
        }

        /// <inheritdoc/>
        public virtual Task<TM> UpdateAsync(TU model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return this.UpdateInternalAsync(model);
        }

        private async Task<TM> InsertInternalAsync(TI model)
        {
            var dbmodel = this.Mapper.Map<T>(model);
            dbmodel.ObjectId = this.ApplicationContext.GuidProvider.Invoke();
            dbmodel.ModifiedBy = this.ApplicationContext.UserContext.UserId;
            dbmodel.ModifiedOn = this.ApplicationContext.DateTimeProvider.Invoke();
            dbmodel.CreatedBy = dbmodel.ModifiedBy;
            dbmodel.CreatedOn = dbmodel.ModifiedOn;
            dbmodel.Id = null;

            await this.Collection.InsertOneAsync(dbmodel, new InsertOneOptions { BypassDocumentValidation = false }).ConfigureAwait(false);

            return this.Mapper.Map<TM>(dbmodel);
        }

        private async Task<TM> UpdateInternalAsync(TU model)
        {
            Guid objectId = model.Id.ToNewGuid();

            var dbmodel = this.Mapper.Map<T>(model);
            dbmodel.ModifiedBy = this.ApplicationContext.UserContext.UserId;
            dbmodel.ModifiedOn = this.ApplicationContext.DateTimeProvider.Invoke();

            var filterDefinition = new FilterDefinitionBuilder<T>().Eq(m => m.ObjectId, objectId);
            var updateDefinition = new UpdateDefinitionBuilder<T>()
                .Set(s => s.Name, model.Name)
                .Set(s => s.Description, model.Description)
                .Set(s => s.ModifiedBy, dbmodel.ModifiedBy)
                .Set(s => s.ModifiedOn, dbmodel.ModifiedOn);
            var updateOptions = new UpdateOptions
            {
                BypassDocumentValidation = false,
                IsUpsert = false,
            };

            var result = await this.Collection.UpdateOneAsync(filterDefinition, updateDefinition, updateOptions).ConfigureAwait(false);

            if (!result.IsAcknowledged)
            {
                throw new UpdateUnsuccessfulException();
            }

            return this.Mapper.Map<TM>(dbmodel);
        }
    }
}
