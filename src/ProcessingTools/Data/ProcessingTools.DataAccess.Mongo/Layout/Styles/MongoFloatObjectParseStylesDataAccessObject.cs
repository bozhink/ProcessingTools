// <copyright file="MongoFloatObjectParseStylesDataAccessObject.cs" company="ProcessingTools">
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
    using ProcessingTools.Contracts.DataAccess.Layout.Styles;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.Floats;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Layout.Styles.Floats;
    using ProcessingTools.Data.Models.Mongo.Layout.Styles;
    using ProcessingTools.Extensions;

    /// <summary>
    /// MongoDB implementation of <see cref="IFloatObjectParseStylesDataAccessObject"/>.
    /// </summary>
    public class MongoFloatObjectParseStylesDataAccessObject : MongoStylesDataAccessObject<IFloatObjectParseStyleDataTransferObject, IFloatObjectDetailsParseStyleDataTransferObject, IFloatObjectInsertParseStyleModel, IFloatObjectUpdateParseStyleModel, FloatObjectParseStyle>, IFloatObjectParseStylesDataAccessObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MongoFloatObjectParseStylesDataAccessObject"/> class.
        /// </summary>
        /// <param name="collection">Instance of <see cref="IMongoCollection{FloatObjectParseStyle}"/>.</param>
        /// <param name="applicationContext">Application context.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        public MongoFloatObjectParseStylesDataAccessObject(IMongoCollection<FloatObjectParseStyle> collection, IApplicationContext applicationContext, IMapper mapper)
            : base(collection, applicationContext, mapper)
        {
        }

        /// <inheritdoc/>
        public override async Task<IList<IFloatObjectParseStyleDataTransferObject>> SelectAsync(int skip, int take)
        {
            var parseStyles = await this.Collection.Find(Builders<FloatObjectParseStyle>.Filter.Empty)
                .Project(Builders<FloatObjectParseStyle>.Projection.Exclude(s => s.Script))
                .As<FloatObjectParseStyle>()
                .SortBy(p => p.Name)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (parseStyles is null || !parseStyles.Any())
            {
                return Array.Empty<IFloatObjectParseStyleDataTransferObject>();
            }

            return parseStyles.Select(this.Mapper.Map<IFloatObjectParseStyleDataTransferObject>).ToArray();
        }

        /// <inheritdoc/>
        public override async Task<IList<IFloatObjectDetailsParseStyleDataTransferObject>> SelectDetailsAsync(int skip, int take)
        {
            var parseStyles = await this.Collection.Find(Builders<FloatObjectParseStyle>.Filter.Empty)
                .SortBy(p => p.Name)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (parseStyles is null || !parseStyles.Any())
            {
                return Array.Empty<IFloatObjectDetailsParseStyleDataTransferObject>();
            }

            return parseStyles.Select(this.Mapper.Map<IFloatObjectDetailsParseStyleDataTransferObject>).ToArray();
        }

        /// <inheritdoc/>
        public override async Task<IFloatObjectParseStyleDataTransferObject> UpdateAsync(IFloatObjectUpdateParseStyleModel model)
        {
            if (model is null)
            {
                return null;
            }

            Guid objectId = model.Id.ToNewGuid();

            var parseStyle = this.Mapper.Map<IFloatObjectUpdateParseStyleModel, FloatObjectParseStyle>(model);
            parseStyle.ModifiedBy = this.ApplicationContext.UserContext.UserId;
            parseStyle.ModifiedOn = this.ApplicationContext.DateTimeProvider.Invoke();

            var filterDefinition = new FilterDefinitionBuilder<FloatObjectParseStyle>().Eq(m => m.ObjectId, objectId);
            var updateDefinition = new UpdateDefinitionBuilder<FloatObjectParseStyle>()
                .Set(s => s.Name, model.Name)
                .Set(s => s.Description, model.Description)
                .Set(s => s.FloatReferenceType, model.FloatReferenceType)
                .Set(s => s.Script, model.Script)
                .Set(s => s.FloatObjectXPath, model.FloatObjectXPath)
                .Set(s => s.ModifiedBy, parseStyle.ModifiedBy)
                .Set(s => s.ModifiedOn, parseStyle.ModifiedOn);
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

            return this.Mapper.Map<IFloatObjectParseStyleDataTransferObject>(parseStyle);
        }
    }
}
