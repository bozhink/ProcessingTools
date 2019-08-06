﻿// <copyright file="MongoReferenceTagStylesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
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
    using ProcessingTools.Contracts.DataAccess.Layout.Styles;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.References;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Layout.Styles.References;
    using ProcessingTools.Data.Models.Mongo.Layout;
    using ProcessingTools.Data.Mongo;
    using ProcessingTools.Data.Mongo.Abstractions;
    using ProcessingTools.Extensions;

    /// <summary>
    /// MongoDB implementation of <see cref="IReferenceTagStylesDataAccessObject"/>.
    /// </summary>
    public class MongoReferenceTagStylesDataAccessObject : MongoDataAccessObjectBase<ReferenceTagStyle>, IReferenceTagStylesDataAccessObject
    {
        private readonly IApplicationContext applicationContext;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoReferenceTagStylesDataAccessObject"/> class.
        /// </summary>
        /// <param name="databaseProvider">Instance of <see cref="IMongoDatabaseProvider"/>.</param>
        /// <param name="applicationContext">Application context.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        public MongoReferenceTagStylesDataAccessObject(IMongoDatabaseProvider databaseProvider, IApplicationContext applicationContext, IMapper mapper)
            : base(databaseProvider)
        {
            this.applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            this.CollectionSettings = new MongoCollectionSettings
            {
                AssignIdOnInsert = true,
                GuidRepresentation = MongoDB.Bson.GuidRepresentation.Unspecified,
                WriteConcern = new WriteConcern(WriteConcern.WMajority.W),
            };
        }

        /// <inheritdoc/>
        public async Task<IReferenceTagStyleDataTransferObject> GetByIdAsync(object id) => await this.GetDetailsByIdAsync(id).ConfigureAwait(false);

        /// <inheritdoc/>
        public async Task<IReferenceDetailsTagStyleDataTransferObject> GetDetailsByIdAsync(object id)
        {
            if (id == null)
            {
                return null;
            }

            Guid objectId = id.ToNewGuid();

            var tagStyle = await this.Collection.Find(s => s.ObjectId == objectId).FirstOrDefaultAsync().ConfigureAwait(false);

            return tagStyle;
        }

        /// <inheritdoc/>
        public async Task<object> DeleteAsync(object id)
        {
            if (id == null)
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
        public async Task<IReferenceTagStyleDataTransferObject> InsertAsync(IReferenceInsertTagStyleModel model)
        {
            if (model == null)
            {
                return null;
            }

            var tagStyle = this.mapper.Map<IReferenceInsertTagStyleModel, ReferenceTagStyle>(model);
            tagStyle.ObjectId = this.applicationContext.GuidProvider.Invoke();
            tagStyle.ModifiedBy = this.applicationContext.UserContext.UserId;
            tagStyle.ModifiedOn = this.applicationContext.DateTimeProvider.Invoke();
            tagStyle.CreatedBy = tagStyle.ModifiedBy;
            tagStyle.CreatedOn = tagStyle.ModifiedOn;
            tagStyle.Id = null;

            await this.Collection.InsertOneAsync(tagStyle, new InsertOneOptions { BypassDocumentValidation = false }).ConfigureAwait(false);

            return tagStyle;
        }

        /// <inheritdoc/>
        public async Task<IList<IReferenceTagStyleDataTransferObject>> SelectAsync(int skip, int take)
        {
            var tagStyles = await this.Collection.Find(Builders<ReferenceTagStyle>.Filter.Empty)
                .SortBy(p => p.Name)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (tagStyles == null || !tagStyles.Any())
            {
                return Array.Empty<IReferenceTagStyleDataTransferObject>();
            }

            return tagStyles.ToArray<IReferenceTagStyleDataTransferObject>();
        }

        /// <inheritdoc/>
        public async Task<IList<IReferenceDetailsTagStyleDataTransferObject>> SelectDetailsAsync(int skip, int take)
        {
            var tagStyles = await this.Collection.Find(Builders<ReferenceTagStyle>.Filter.Empty)
                .SortBy(p => p.Name)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (tagStyles == null || !tagStyles.Any())
            {
                return Array.Empty<IReferenceDetailsTagStyleDataTransferObject>();
            }

            return tagStyles.ToArray<IReferenceDetailsTagStyleDataTransferObject>();
        }

        /// <inheritdoc/>
        public Task<long> SelectCountAsync()
        {
            return this.Collection.CountDocumentsAsync(Builders<ReferenceTagStyle>.Filter.Empty);
        }

        /// <inheritdoc/>
        public async Task<IReferenceTagStyleDataTransferObject> UpdateAsync(IReferenceUpdateTagStyleModel model)
        {
            if (model == null)
            {
                return null;
            }

            Guid objectId = model.Id.ToNewGuid();

            var tagStyle = this.mapper.Map<IReferenceUpdateTagStyleModel, ReferenceTagStyle>(model);
            tagStyle.ModifiedBy = this.applicationContext.UserContext.UserId;
            tagStyle.ModifiedOn = this.applicationContext.DateTimeProvider.Invoke();

            var filterDefinition = new FilterDefinitionBuilder<ReferenceTagStyle>().Eq(m => m.ObjectId, objectId);
            var updateDefinition = new UpdateDefinitionBuilder<ReferenceTagStyle>()
                .Set(s => s.Name, model.Name)
                .Set(s => s.Description, model.Description)
                .Set(s => s.Script, model.Script)
                .Set(s => s.ReferenceXPath, model.ReferenceXPath)
                .Set(s => s.TargetXPath, model.TargetXPath)
                .Set(s => s.ModifiedBy, tagStyle.ModifiedBy)
                .Set(s => s.ModifiedOn, tagStyle.ModifiedOn);
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

            return tagStyle;
        }

        /// <inheritdoc/>
        public async Task<IIdentifiedStyleDataTransferObject> GetStyleByIdAsync(object id)
        {
            if (id == null)
            {
                return null;
            }

            Guid objectId = id.ToNewGuid();

            var style = await this.Collection.Find(s => s.ObjectId == objectId)
                .Project(s => new StyleDataModel
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
            var data = await this.Collection.Find(Builders<ReferenceTagStyle>.Filter.Empty)
                .Project(s => new StyleDataModel
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
    }
}