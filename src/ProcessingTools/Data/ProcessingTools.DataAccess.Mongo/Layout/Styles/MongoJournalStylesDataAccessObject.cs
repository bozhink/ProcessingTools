// <copyright file="MongoJournalStylesDataAccessObject.cs" company="ProcessingTools">
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
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.Journals;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.References;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Layout.Styles.Journals;
    using ProcessingTools.Data.Models.Mongo.Layout.Styles;
    using ProcessingTools.Data.Mongo;
    using ProcessingTools.Extensions;

    /// <summary>
    /// MongoDB implementation of <see cref="IJournalStylesDataAccessObject"/>.
    /// </summary>
    public class MongoJournalStylesDataAccessObject : MongoStylesDataAccessObject<IJournalStyleDataTransferObject, IJournalDetailsStyleDataTransferObject, IJournalInsertStyleModel, IJournalUpdateStyleModel, JournalStyle>, IJournalStylesDataAccessObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MongoJournalStylesDataAccessObject"/> class.
        /// </summary>
        /// <param name="collection">Instance of <see cref="IMongoCollection{JournalStyle}"/>.</param>
        /// <param name="applicationContext">Application context.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        public MongoJournalStylesDataAccessObject(IMongoCollection<JournalStyle> collection, IApplicationContext applicationContext, IMapper mapper)
            : base(collection, applicationContext, mapper)
        {
        }

        /// <inheritdoc/>
        public override async Task<IList<IJournalStyleDataTransferObject>> SelectAsync(int skip, int take)
        {
            var journalStyles = await this.Collection.Find(Builders<JournalStyle>.Filter.Empty)
                .SortBy(s => s.Name)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (journalStyles is null || !journalStyles.Any())
            {
                return Array.Empty<IJournalStyleDataTransferObject>();
            }

            return journalStyles.Select(this.Mapper.Map<IJournalStyleDataTransferObject>).ToArray();
        }

        /// <inheritdoc/>
        public override async Task<IList<IJournalDetailsStyleDataTransferObject>> SelectDetailsAsync(int skip, int take)
        {
            var query = this.GetDetailsLookup(this.Collection.Aggregate())
                .SortBy(s => s.Name)
                .Skip(skip)
                .Limit(take);

            var journalStyles = await query.ToListAsync().ConfigureAwait(false);

            if (journalStyles is null || !journalStyles.Any())
            {
                return Array.Empty<IJournalDetailsStyleDataTransferObject>();
            }

            return journalStyles.Select(this.Mapper.Map<IJournalDetailsStyleDataTransferObject>).ToArray();
        }

        /// <inheritdoc/>
        public override async Task<IJournalStyleDataTransferObject> UpdateAsync(IJournalUpdateStyleModel model)
        {
            if (model is null)
            {
                return null;
            }

            Guid objectId = model.Id.ToNewGuid();

            var journalStyle = this.Mapper.Map<IJournalUpdateStyleModel, JournalStyle>(model);
            journalStyle.ModifiedBy = this.ApplicationContext.UserContext.UserId;
            journalStyle.ModifiedOn = this.ApplicationContext.DateTimeProvider.Invoke();

            var filterDefinition = new FilterDefinitionBuilder<JournalStyle>().Eq(m => m.ObjectId, objectId);
            var updateDefinition = new UpdateDefinitionBuilder<JournalStyle>()
                .Set(s => s.Name, model.Name)
                .Set(s => s.Description, model.Description)
                .Set(s => s.FloatObjectParseStyleIds, model.FloatObjectParseStyleIds)
                .Set(s => s.FloatObjectTagStyleIds, model.FloatObjectTagStyleIds)
                .Set(s => s.ReferenceParseStyleIds, model.ReferenceParseStyleIds)
                .Set(s => s.ReferenceTagStyleIds, model.ReferenceTagStyleIds)
                .Set(s => s.ModifiedBy, journalStyle.ModifiedBy)
                .Set(s => s.ModifiedOn, journalStyle.ModifiedOn);
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

            return this.Mapper.Map<IJournalStyleDataTransferObject>(journalStyle);
        }

        /// <inheritdoc/>
        public async Task<IList<IFloatObjectParseStyleDataTransferObject>> GetFloatObjectParseStylesAsync(object id)
        {
            if (id is null)
            {
                return Array.Empty<IFloatObjectParseStyleDataTransferObject>();
            }

            Guid objectId = id.ToNewGuid();

            var query = this.Collection.Aggregate().Match(s => s.ObjectId == objectId)
                .Lookup<FloatObjectParseStyle, JournalStyle>(
                    foreignCollectionName: MongoCollectionNameFactory.Create<FloatObjectParseStyle>(),
                    localField: nameof(JournalStyle.FloatObjectParseStyleIds),
                    foreignField: nameof(FloatObjectParseStyle.ObjectId),
                    @as: nameof(JournalStyle.FloatObjectParseStyles));

            var journalStyle = await query.FirstOrDefaultAsync().ConfigureAwait(false);

            if (journalStyle?.FloatObjectParseStyles is null || !journalStyle.FloatObjectParseStyles.Any())
            {
                return Array.Empty<IFloatObjectParseStyleDataTransferObject>();
            }

            return journalStyle.FloatObjectParseStyles.Select(this.Mapper.Map<IFloatObjectParseStyleDataTransferObject>).ToArray();
        }

        /// <inheritdoc/>
        public async Task<IList<IFloatObjectTagStyleDataTransferObject>> GetFloatObjectTagStylesAsync(object id)
        {
            if (id is null)
            {
                return Array.Empty<IFloatObjectTagStyleDataTransferObject>();
            }

            Guid objectId = id.ToNewGuid();

            var query = this.Collection.Aggregate().Match(s => s.ObjectId == objectId)
                .Lookup<FloatObjectTagStyle, JournalStyle>(
                    foreignCollectionName: MongoCollectionNameFactory.Create<FloatObjectTagStyle>(),
                    localField: nameof(JournalStyle.FloatObjectTagStyleIds),
                    foreignField: nameof(FloatObjectTagStyle.ObjectId),
                    @as: nameof(JournalStyle.FloatObjectTagStyles));

            var journalStyle = await query.FirstOrDefaultAsync().ConfigureAwait(false);

            if (journalStyle?.FloatObjectTagStyles is null || !journalStyle.FloatObjectTagStyles.Any())
            {
                return Array.Empty<IFloatObjectTagStyleDataTransferObject>();
            }

            return journalStyle.FloatObjectTagStyles.Select(this.Mapper.Map<IFloatObjectTagStyleDataTransferObject>).ToArray();
        }

        /// <inheritdoc/>
        public async Task<IList<IReferenceParseStyleDataTransferObject>> GetReferenceParseStylesAsync(object id)
        {
            if (id is null)
            {
                return Array.Empty<IReferenceParseStyleDataTransferObject>();
            }

            Guid objectId = id.ToNewGuid();

            var query = this.Collection.Aggregate().Match(s => s.ObjectId == objectId)
                .Lookup<ReferenceParseStyle, JournalStyle>(
                    foreignCollectionName: MongoCollectionNameFactory.Create<ReferenceParseStyle>(),
                    localField: nameof(JournalStyle.ReferenceParseStyleIds),
                    foreignField: nameof(ReferenceParseStyle.ObjectId),
                    @as: nameof(JournalStyle.ReferenceParseStyles));

            var journalStyle = await query.FirstOrDefaultAsync().ConfigureAwait(false);

            if (journalStyle?.ReferenceParseStyles is null || !journalStyle.ReferenceParseStyles.Any())
            {
                return Array.Empty<IReferenceParseStyleDataTransferObject>();
            }

            return journalStyle.ReferenceParseStyles.Select(this.Mapper.Map<IReferenceParseStyleDataTransferObject>).ToArray();
        }

        /// <inheritdoc/>
        public async Task<IList<IReferenceTagStyleDataTransferObject>> GetReferenceTagStylesAsync(object id)
        {
            if (id is null)
            {
                return Array.Empty<IReferenceTagStyleDataTransferObject>();
            }

            Guid objectId = id.ToNewGuid();

            var query = this.Collection.Aggregate().Match(s => s.ObjectId == objectId)
                .Lookup<ReferenceTagStyle, JournalStyle>(
                    foreignCollectionName: MongoCollectionNameFactory.Create<ReferenceTagStyle>(),
                    localField: nameof(JournalStyle.ReferenceTagStyleIds),
                    foreignField: nameof(ReferenceTagStyle.ObjectId),
                    @as: nameof(JournalStyle.ReferenceTagStyles));

            var journalStyle = await query.FirstOrDefaultAsync().ConfigureAwait(false);

            if (journalStyle?.ReferenceTagStyles is null || !journalStyle.ReferenceTagStyles.Any())
            {
                return Array.Empty<IReferenceTagStyleDataTransferObject>();
            }

            return journalStyle?.ReferenceTagStyles.Select(this.Mapper.Map<IReferenceTagStyleDataTransferObject>).ToArray();
        }

        private IAggregateFluent<JournalStyle> GetDetailsLookup(IAggregateFluent<JournalStyle> query)
        {
            return query
                .Lookup<FloatObjectParseStyle, JournalStyle>(
                    foreignCollectionName: MongoCollectionNameFactory.Create<FloatObjectParseStyle>(),
                    localField: nameof(JournalStyle.FloatObjectParseStyleIds),
                    foreignField: nameof(FloatObjectParseStyle.ObjectId),
                    @as: nameof(JournalStyle.FloatObjectParseStyles))
                .Lookup<FloatObjectTagStyle, JournalStyle>(
                    foreignCollectionName: MongoCollectionNameFactory.Create<FloatObjectTagStyle>(),
                    localField: nameof(JournalStyle.FloatObjectTagStyleIds),
                    foreignField: nameof(FloatObjectTagStyle.ObjectId),
                    @as: nameof(JournalStyle.FloatObjectTagStyles))
                .Lookup<ReferenceParseStyle, JournalStyle>(
                    foreignCollectionName: MongoCollectionNameFactory.Create<ReferenceParseStyle>(),
                    localField: nameof(JournalStyle.ReferenceParseStyleIds),
                    foreignField: nameof(ReferenceParseStyle.ObjectId),
                    @as: nameof(JournalStyle.ReferenceParseStyles))
                .Lookup<ReferenceTagStyle, JournalStyle>(
                    foreignCollectionName: MongoCollectionNameFactory.Create<ReferenceTagStyle>(),
                    localField: nameof(JournalStyle.ReferenceTagStyleIds),
                    foreignField: nameof(ReferenceTagStyle.ObjectId),
                    @as: nameof(JournalStyle.ReferenceTagStyles));
        }
    }
}
