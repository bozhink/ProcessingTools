// <copyright file="MongoReferenceTagStylesDataAccessObject.cs" company="ProcessingTools">
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
    /// MongoDB implementation of <see cref="IReferenceTagStylesDataAccessObject"/>.
    /// </summary>
    public class MongoReferenceTagStylesDataAccessObject : MongoStylesDataAccessObject<IReferenceTagStyleDataTransferObject, IReferenceDetailsTagStyleDataTransferObject, IReferenceInsertTagStyleModel, IReferenceUpdateTagStyleModel, ReferenceTagStyle>, IReferenceTagStylesDataAccessObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MongoReferenceTagStylesDataAccessObject"/> class.
        /// </summary>
        /// <param name="collection">Instance of <see cref="IMongoCollection{ReferenceTagStyle}"/>.</param>
        /// <param name="applicationContext">Application context.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        public MongoReferenceTagStylesDataAccessObject(IMongoCollection<ReferenceTagStyle> collection, IApplicationContext applicationContext, IMapper mapper)
            : base(collection, applicationContext, mapper)
        {
        }

        /// <inheritdoc/>
        public override async Task<IList<IReferenceTagStyleDataTransferObject>> SelectAsync(int skip, int take)
        {
            var tagStyles = await this.Collection.Find(Builders<ReferenceTagStyle>.Filter.Empty)
                .Project(Builders<ReferenceTagStyle>.Projection.Exclude(s => s.Script))
                .As<ReferenceTagStyle>()
                .SortBy(p => p.Name)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (tagStyles is null || !tagStyles.Any())
            {
                return Array.Empty<IReferenceTagStyleDataTransferObject>();
            }

            return tagStyles.Select(this.Mapper.Map<IReferenceTagStyleDataTransferObject>).ToArray();
        }

        /// <inheritdoc/>
        public override async Task<IList<IReferenceDetailsTagStyleDataTransferObject>> SelectDetailsAsync(int skip, int take)
        {
            var tagStyles = await this.Collection.Find(Builders<ReferenceTagStyle>.Filter.Empty)
                .SortBy(p => p.Name)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (tagStyles is null || !tagStyles.Any())
            {
                return Array.Empty<IReferenceDetailsTagStyleDataTransferObject>();
            }

            return tagStyles.Select(this.Mapper.Map<IReferenceDetailsTagStyleDataTransferObject>).ToArray();
        }

        /// <inheritdoc/>
        public override async Task<IReferenceTagStyleDataTransferObject> UpdateAsync(IReferenceUpdateTagStyleModel model)
        {
            if (model is null)
            {
                return null;
            }

            Guid objectId = model.Id.ToNewGuid();

            var tagStyle = this.Mapper.Map<IReferenceUpdateTagStyleModel, ReferenceTagStyle>(model);
            tagStyle.ModifiedBy = this.ApplicationContext.UserContext.UserId;
            tagStyle.ModifiedOn = this.ApplicationContext.DateTimeProvider.Invoke();

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

            return this.Mapper.Map<IReferenceTagStyleDataTransferObject>(tagStyle);
        }
    }
}
