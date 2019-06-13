﻿// <copyright file="MongoReferenceParseStylesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Mongo.Layout
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using MongoDB.Driver;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.DataAccess.Layout.Styles;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.References;
    using ProcessingTools.Contracts.Models.Layout.Styles.References;
    using ProcessingTools.Data.Models.Mongo.Layout;
    using ProcessingTools.Data.Mongo.Abstractions;
    using ProcessingTools.Extensions;

    /// <summary>
    /// MongoDB implementation of <see cref="IReferenceParseStylesDataAccessObject"/>.
    /// </summary>
    public class MongoReferenceParseStylesDataAccessObject : MongoDataAccessObjectBase<ReferenceParseStyle>, IReferenceParseStylesDataAccessObject
    {
        private readonly IApplicationContext applicationContext;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoReferenceParseStylesDataAccessObject"/> class.
        /// </summary>
        /// <param name="databaseProvider">Instance of <see cref="IMongoDatabaseProvider"/>.</param>
        /// <param name="applicationContext">Application context.</param>
        public MongoReferenceParseStylesDataAccessObject(IMongoDatabaseProvider databaseProvider, IApplicationContext applicationContext)
            : base(databaseProvider)
        {
            this.applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IReferenceInsertParseStyleModel, ReferenceParseStyle>();
                c.CreateMap<IReferenceUpdateParseStyleModel, ReferenceParseStyle>();
            });

            this.mapper = mapperConfiguration.CreateMapper();

            this.CollectionSettings = new MongoCollectionSettings
            {
                AssignIdOnInsert = true,
                GuidRepresentation = MongoDB.Bson.GuidRepresentation.Unspecified,
                WriteConcern = new WriteConcern(WriteConcern.WMajority.W),
            };
        }

        /// <inheritdoc/>
        public async Task<IReferenceParseStyleDataTransferObject> GetByIdAsync(object id) => await this.GetDetailsByIdAsync(id).ConfigureAwait(false);

        /// <inheritdoc/>
        public async Task<IReferenceDetailsParseStyleDataTransferObject> GetDetailsByIdAsync(object id)
        {
            if (id == null)
            {
                return null;
            }

            Guid objectId = id.ToNewGuid();

            var parseStyle = await this.Collection.Find(s => s.ObjectId == objectId).FirstOrDefaultAsync().ConfigureAwait(false);

            return parseStyle;
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
        public async Task<IReferenceParseStyleDataTransferObject> InsertAsync(IReferenceInsertParseStyleModel model)
        {
            if (model == null)
            {
                return null;
            }

            var parseStyle = this.mapper.Map<IReferenceInsertParseStyleModel, ReferenceParseStyle>(model);
            parseStyle.ObjectId = this.applicationContext.GuidProvider.Invoke();
            parseStyle.ModifiedBy = this.applicationContext.UserContext.UserId;
            parseStyle.ModifiedOn = this.applicationContext.DateTimeProvider.Invoke();
            parseStyle.CreatedBy = parseStyle.ModifiedBy;
            parseStyle.CreatedOn = parseStyle.ModifiedOn;
            parseStyle.Id = null;

            await this.Collection.InsertOneAsync(parseStyle, new InsertOneOptions { BypassDocumentValidation = false }).ConfigureAwait(false);

            return parseStyle;
        }

        /// <inheritdoc/>
        public async Task<IList<IReferenceParseStyleDataTransferObject>> SelectAsync(int skip, int take)
        {
            var parseStyles = await this.Collection.Find(Builders<ReferenceParseStyle>.Filter.Empty)
                .SortBy(p => p.Name)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (parseStyles == null || !parseStyles.Any())
            {
                return Array.Empty<IReferenceParseStyleDataTransferObject>();
            }

            return parseStyles.ToArray<IReferenceParseStyleDataTransferObject>();
        }

        /// <inheritdoc/>
        public async Task<IList<IReferenceDetailsParseStyleDataTransferObject>> SelectDetailsAsync(int skip, int take)
        {
            var parseStyles = await this.Collection.Find(Builders<ReferenceParseStyle>.Filter.Empty)
                .SortBy(p => p.Name)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (parseStyles == null || !parseStyles.Any())
            {
                return Array.Empty<IReferenceDetailsParseStyleDataTransferObject>();
            }

            return parseStyles.ToArray<IReferenceDetailsParseStyleDataTransferObject>();
        }

        /// <inheritdoc/>
        public Task<long> SelectCountAsync()
        {
            return this.Collection.CountDocumentsAsync(Builders<ReferenceParseStyle>.Filter.Empty);
        }

        /// <inheritdoc/>
        public async Task<IReferenceParseStyleDataTransferObject> UpdateAsync(IReferenceUpdateParseStyleModel model)
        {
            if (model == null)
            {
                return null;
            }

            Guid objectId = model.Id.ToNewGuid();

            var parseStyle = this.mapper.Map<IReferenceUpdateParseStyleModel, ReferenceParseStyle>(model);
            parseStyle.ModifiedBy = this.applicationContext.UserContext.UserId;
            parseStyle.ModifiedOn = this.applicationContext.DateTimeProvider.Invoke();

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

            return parseStyle;
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
        public async Task<IIdentifiedStyleDataTransferObject[]> GetStylesForSelectAsync()
        {
            var data = await this.Collection.Find(Builders<ReferenceParseStyle>.Filter.Empty)
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
