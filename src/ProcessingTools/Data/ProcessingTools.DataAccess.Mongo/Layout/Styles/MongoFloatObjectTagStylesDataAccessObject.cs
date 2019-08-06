﻿// <copyright file="MongoFloatObjectTagStylesDataAccessObject.cs" company="ProcessingTools">
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
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.Floats;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Layout.Styles.Floats;
    using ProcessingTools.Data.Models.Mongo.Layout;
    using ProcessingTools.Data.Mongo;
    using ProcessingTools.Data.Mongo.Abstractions;
    using ProcessingTools.Extensions;

    /// <summary>
    /// MongoDB implementation of <see cref="IFloatObjectTagStylesDataAccessObject"/>.
    /// </summary>
    public class MongoFloatObjectTagStylesDataAccessObject : MongoDataAccessObjectBase<FloatObjectTagStyle>, IFloatObjectTagStylesDataAccessObject
    {
        private readonly IApplicationContext applicationContext;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoFloatObjectTagStylesDataAccessObject"/> class.
        /// </summary>
        /// <param name="databaseProvider">Instance of <see cref="IMongoDatabaseProvider"/>.</param>
        /// <param name="applicationContext">Application context.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        public MongoFloatObjectTagStylesDataAccessObject(IMongoDatabaseProvider databaseProvider, IApplicationContext applicationContext, IMapper mapper)
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
        public async Task<IFloatObjectTagStyleDataTransferObject> GetByIdAsync(object id) => await this.GetDetailsByIdAsync(id).ConfigureAwait(false);

        /// <inheritdoc/>
        public async Task<IFloatObjectDetailsTagStyleDataTransferObject> GetDetailsByIdAsync(object id)
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
        public async Task<IFloatObjectTagStyleDataTransferObject> InsertAsync(IFloatObjectInsertTagStyleModel model)
        {
            if (model == null)
            {
                return null;
            }

            var tagStyle = this.mapper.Map<IFloatObjectInsertTagStyleModel, FloatObjectTagStyle>(model);
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
        public async Task<IList<IFloatObjectTagStyleDataTransferObject>> SelectAsync(int skip, int take)
        {
            var tagStyles = await this.Collection.Find(Builders<FloatObjectTagStyle>.Filter.Empty)
                .SortBy(p => p.Name)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (tagStyles == null || !tagStyles.Any())
            {
                return Array.Empty<IFloatObjectTagStyleDataTransferObject>();
            }

            return tagStyles.ToArray<IFloatObjectTagStyleDataTransferObject>();
        }

        /// <inheritdoc/>
        public async Task<IList<IFloatObjectDetailsTagStyleDataTransferObject>> SelectDetailsAsync(int skip, int take)
        {
            var tagStyles = await this.Collection.Find(Builders<FloatObjectTagStyle>.Filter.Empty)
                .SortBy(p => p.Name)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (tagStyles == null || !tagStyles.Any())
            {
                return Array.Empty<IFloatObjectDetailsTagStyleDataTransferObject>();
            }

            return tagStyles.ToArray<IFloatObjectDetailsTagStyleDataTransferObject>();
        }

        /// <inheritdoc/>
        public Task<long> SelectCountAsync()
        {
            return this.Collection.CountDocumentsAsync(Builders<FloatObjectTagStyle>.Filter.Empty);
        }

        /// <inheritdoc/>
        public async Task<IFloatObjectTagStyleDataTransferObject> UpdateAsync(IFloatObjectUpdateTagStyleModel model)
        {
            if (model == null)
            {
                return null;
            }

            Guid objectId = model.Id.ToNewGuid();

            var tagStyle = this.mapper.Map<IFloatObjectUpdateTagStyleModel, FloatObjectTagStyle>(model);
            tagStyle.ModifiedBy = this.applicationContext.UserContext.UserId;
            tagStyle.ModifiedOn = this.applicationContext.DateTimeProvider.Invoke();

            var filterDefinition = new FilterDefinitionBuilder<FloatObjectTagStyle>().Eq(m => m.ObjectId, objectId);
            var updateDefinition = new UpdateDefinitionBuilder<FloatObjectTagStyle>()
                .Set(s => s.Name, model.Name)
                .Set(s => s.Description, model.Description)
                .Set(s => s.FloatReferenceType, model.FloatReferenceType)
                .Set(s => s.FloatTypeNameInLabel, model.FloatTypeNameInLabel)
                .Set(s => s.MatchCitationPattern, model.MatchCitationPattern)
                .Set(s => s.InternalReferenceType, model.InternalReferenceType)
                .Set(s => s.ResultantReferenceType, model.ResultantReferenceType)
                .Set(s => s.FloatObjectXPath, model.FloatObjectXPath)
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
            var data = await this.Collection.Find(Builders<FloatObjectTagStyle>.Filter.Empty)
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