// <copyright file="MongoFloatObjectTagStylesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
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
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.Floats;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Layout.Styles.Floats;
    using ProcessingTools.Data.Models.Mongo.Layout.Styles;
    using ProcessingTools.Extensions;

    /// <summary>
    /// MongoDB implementation of <see cref="IFloatObjectTagStylesDataAccessObject"/>.
    /// </summary>
    public class MongoFloatObjectTagStylesDataAccessObject : MongoStylesDataAccessObject<IFloatObjectTagStyleDataTransferObject, IFloatObjectDetailsTagStyleDataTransferObject, IFloatObjectInsertTagStyleModel, IFloatObjectUpdateTagStyleModel, FloatObjectTagStyle>, IFloatObjectTagStylesDataAccessObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MongoFloatObjectTagStylesDataAccessObject"/> class.
        /// </summary>
        /// <param name="collection">Instance of <see cref="IMongoCollection{FloatObjectTagStyle}"/>.</param>
        /// <param name="applicationContext">Application context.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        public MongoFloatObjectTagStylesDataAccessObject(IMongoCollection<FloatObjectTagStyle> collection, IApplicationContext applicationContext, IMapper mapper)
            : base(collection, applicationContext, mapper)
        {
        }

        /// <inheritdoc/>
        public override async Task<IList<IFloatObjectTagStyleDataTransferObject>> SelectAsync(int skip, int take)
        {
            var tagStyles = await this.Collection.Find(Builders<FloatObjectTagStyle>.Filter.Empty)
                .As<FloatObjectTagStyle>()
                .SortBy(p => p.Name)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (tagStyles is null || !tagStyles.Any())
            {
                return Array.Empty<IFloatObjectTagStyleDataTransferObject>();
            }

            return tagStyles.Select(this.Mapper.Map<IFloatObjectTagStyleDataTransferObject>).ToArray();
        }

        /// <inheritdoc/>
        public override async Task<IList<IFloatObjectDetailsTagStyleDataTransferObject>> SelectDetailsAsync(int skip, int take)
        {
            var tagStyles = await this.Collection.Find(Builders<FloatObjectTagStyle>.Filter.Empty)
                .SortBy(p => p.Name)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (tagStyles is null || !tagStyles.Any())
            {
                return Array.Empty<IFloatObjectDetailsTagStyleDataTransferObject>();
            }

            return tagStyles.Select(this.Mapper.Map<IFloatObjectDetailsTagStyleDataTransferObject>).ToArray();
        }

        /// <inheritdoc/>
        public override async Task<IFloatObjectTagStyleDataTransferObject> UpdateAsync(IFloatObjectUpdateTagStyleModel model)
        {
            if (model is null)
            {
                return null;
            }

            Guid objectId = model.Id.ToNewGuid();

            var tagStyle = this.Mapper.Map<IFloatObjectUpdateTagStyleModel, FloatObjectTagStyle>(model);
            tagStyle.ModifiedBy = this.ApplicationContext.UserContext.UserId;
            tagStyle.ModifiedOn = this.ApplicationContext.DateTimeProvider.Invoke();

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

            return this.Mapper.Map<IFloatObjectTagStyleDataTransferObject>(tagStyle);
        }
    }
}
