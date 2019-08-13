// <copyright file="MongoReferenceParseStylesDataAccessObject.cs" company="ProcessingTools">
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
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.References;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Layout.Styles.References;
    using ProcessingTools.Data.Models.Mongo.Layout.Styles;
    using ProcessingTools.Extensions;

    /// <summary>
    /// MongoDB implementation of <see cref="IReferenceParseStylesDataAccessObject"/>.
    /// </summary>
    public class MongoReferenceParseStylesDataAccessObject : MongoStylesDataAccessObject<IReferenceParseStyleDataTransferObject, IReferenceDetailsParseStyleDataTransferObject, IReferenceInsertParseStyleModel, IReferenceUpdateParseStyleModel, ReferenceParseStyle>, IReferenceParseStylesDataAccessObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MongoReferenceParseStylesDataAccessObject"/> class.
        /// </summary>
        /// <param name="collection">Instance of <see cref="IMongoCollection{ReferenceParseStyle}"/>.</param>
        /// <param name="applicationContext">Application context.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        public MongoReferenceParseStylesDataAccessObject(IMongoCollection<ReferenceParseStyle> collection, IApplicationContext applicationContext, IMapper mapper)
            : base(collection, applicationContext, mapper)
        {
        }

        /// <inheritdoc/>
        public override async Task<IList<IReferenceParseStyleDataTransferObject>> SelectAsync(int skip, int take)
        {
            var parseStyles = await this.Collection.Find(Builders<ReferenceParseStyle>.Filter.Empty)
                .Project(Builders<ReferenceParseStyle>.Projection.Exclude(s => s.Script))
                .As<ReferenceParseStyle>()
                .SortBy(p => p.Name)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (parseStyles is null || !parseStyles.Any())
            {
                return Array.Empty<IReferenceParseStyleDataTransferObject>();
            }

            return parseStyles.Select(this.Mapper.Map<IReferenceParseStyleDataTransferObject>).ToArray();
        }

        /// <inheritdoc/>
        public override async Task<IList<IReferenceDetailsParseStyleDataTransferObject>> SelectDetailsAsync(int skip, int take)
        {
            var parseStyles = await this.Collection.Find(Builders<ReferenceParseStyle>.Filter.Empty)
                .SortBy(p => p.Name)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (parseStyles is null || !parseStyles.Any())
            {
                return Array.Empty<IReferenceDetailsParseStyleDataTransferObject>();
            }

            return parseStyles.Select(this.Mapper.Map<IReferenceDetailsParseStyleDataTransferObject>).ToArray();
        }

        /// <inheritdoc/>
        public override async Task<IReferenceParseStyleDataTransferObject> UpdateAsync(IReferenceUpdateParseStyleModel model)
        {
            if (model is null)
            {
                return null;
            }

            Guid objectId = model.Id.ToNewGuid();

            var parseStyle = this.Mapper.Map<IReferenceUpdateParseStyleModel, ReferenceParseStyle>(model);
            parseStyle.ModifiedBy = this.ApplicationContext.UserContext.UserId;
            parseStyle.ModifiedOn = this.ApplicationContext.DateTimeProvider.Invoke();

            var filterDefinition = new FilterDefinitionBuilder<ReferenceParseStyle>().Eq(m => m.ObjectId, objectId);
            var updateDefinition = new UpdateDefinitionBuilder<ReferenceParseStyle>()
                .Set(s => s.Name, model.Name)
                .Set(s => s.Description, model.Description)
                .Set(s => s.Script, model.Script)
                .Set(s => s.ReferenceXPath, model.ReferenceXPath)
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

            return this.Mapper.Map<IReferenceParseStyleDataTransferObject>(parseStyle);
        }
    }
}
