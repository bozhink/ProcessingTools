// <copyright file="MongoJournalStylesDataAccessObject.cs" company="ProcessingTools">
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
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.Journals;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.References;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Layout.Styles.Journals;
    using ProcessingTools.Data.Models.Mongo.Layout;
    using ProcessingTools.Data.Mongo;
    using ProcessingTools.Data.Mongo.Abstractions;
    using ProcessingTools.Extensions;

    /// <summary>
    /// MongoDB implementation of <see cref="IJournalStylesDataAccessObject"/>.
    /// </summary>
    public class MongoJournalStylesDataAccessObject : MongoDataAccessObjectBase<JournalStyle>, IJournalStylesDataAccessObject
    {
        private readonly IApplicationContext applicationContext;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoJournalStylesDataAccessObject"/> class.
        /// </summary>
        /// <param name="databaseProvider">Instance of <see cref="IMongoDatabaseProvider"/>.</param>
        /// <param name="applicationContext">Application context.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        public MongoJournalStylesDataAccessObject(IMongoDatabaseProvider databaseProvider, IApplicationContext applicationContext, IMapper mapper)
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
        public async Task<IJournalStyleDataTransferObject> GetByIdAsync(object id)
        {
            if (id == null)
            {
                return null;
            }

            Guid objectId = id.ToNewGuid();

            var journalStyle = await this.Collection.Find(s => s.ObjectId == objectId).FirstOrDefaultAsync().ConfigureAwait(false);

            return journalStyle;
        }

        /// <inheritdoc/>
        public async Task<IJournalDetailsStyleDataTransferObject> GetDetailsByIdAsync(object id)
        {
            if (id == null)
            {
                return null;
            }

            Guid objectId = id.ToNewGuid();

            var query = this.GetDetailsLookup(this.Collection.Aggregate().Match(s => s.ObjectId == objectId));

            var journalStyle = await query.FirstOrDefaultAsync().ConfigureAwait(false);

            return journalStyle;
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
        public async Task<IJournalStyleDataTransferObject> InsertAsync(IJournalInsertStyleModel model)
        {
            if (model == null)
            {
                return null;
            }

            var journalStyle = this.mapper.Map<IJournalInsertStyleModel, JournalStyle>(model);
            journalStyle.ObjectId = this.applicationContext.GuidProvider.Invoke();
            journalStyle.ModifiedBy = this.applicationContext.UserContext.UserId;
            journalStyle.ModifiedOn = this.applicationContext.DateTimeProvider.Invoke();
            journalStyle.CreatedBy = journalStyle.ModifiedBy;
            journalStyle.CreatedOn = journalStyle.ModifiedOn;
            journalStyle.Id = null;

            await this.Collection.InsertOneAsync(journalStyle, new InsertOneOptions { BypassDocumentValidation = false }).ConfigureAwait(false);

            return journalStyle;
        }

        /// <inheritdoc/>
        public async Task<IList<IJournalStyleDataTransferObject>> SelectAsync(int skip, int take)
        {
            var journalStyles = await this.Collection.Find(Builders<JournalStyle>.Filter.Empty)
                .SortBy(s => s.Name)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (journalStyles == null || !journalStyles.Any())
            {
                return Array.Empty<IJournalStyleDataTransferObject>();
            }

            return journalStyles.ToArray<IJournalStyleDataTransferObject>();
        }

        /// <inheritdoc/>
        public async Task<IList<IJournalDetailsStyleDataTransferObject>> SelectDetailsAsync(int skip, int take)
        {
            var query = this.GetDetailsLookup(this.Collection.Aggregate())
                .SortBy(s => s.Name)
                .Skip(skip)
                .Limit(take);

            var journalStyles = await query.ToListAsync().ConfigureAwait(false);

            if (journalStyles == null || !journalStyles.Any())
            {
                return Array.Empty<IJournalDetailsStyleDataTransferObject>();
            }

            return journalStyles.ToArray<IJournalDetailsStyleDataTransferObject>();
        }

        /// <inheritdoc/>
        public Task<long> SelectCountAsync()
        {
            return this.Collection.CountDocumentsAsync(Builders<JournalStyle>.Filter.Empty);
        }

        /// <inheritdoc/>
        public async Task<IJournalStyleDataTransferObject> UpdateAsync(IJournalUpdateStyleModel model)
        {
            if (model == null)
            {
                return null;
            }

            Guid objectId = model.Id.ToNewGuid();

            var journalStyle = this.mapper.Map<IJournalUpdateStyleModel, JournalStyle>(model);
            journalStyle.ModifiedBy = this.applicationContext.UserContext.UserId;
            journalStyle.ModifiedOn = this.applicationContext.DateTimeProvider.Invoke();

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

            return journalStyle;
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
            var data = await this.Collection.Find(Builders<JournalStyle>.Filter.Empty)
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

        /// <inheritdoc/>
        public async Task<IList<IFloatObjectParseStyleDataTransferObject>> GetFloatObjectParseStylesAsync(object id)
        {
            if (id == null)
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

            return journalStyle?.FloatObjectParseStyles?.ToArray<IFloatObjectParseStyleDataTransferObject>() ?? Array.Empty<IFloatObjectParseStyleDataTransferObject>();
        }

        /// <inheritdoc/>
        public async Task<IList<IFloatObjectTagStyleDataTransferObject>> GetFloatObjectTagStylesAsync(object id)
        {
            if (id == null)
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

            return journalStyle?.FloatObjectTagStyles?.ToArray<IFloatObjectTagStyleDataTransferObject>() ?? Array.Empty<IFloatObjectTagStyleDataTransferObject>();
        }

        /// <inheritdoc/>
        public async Task<IList<IReferenceParseStyleDataTransferObject>> GetReferenceParseStylesAsync(object id)
        {
            if (id == null)
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

            return journalStyle?.ReferenceParseStyles?.ToArray<IReferenceParseStyleDataTransferObject>() ?? Array.Empty<IReferenceParseStyleDataTransferObject>();
        }

        /// <inheritdoc/>
        public async Task<IList<IReferenceTagStyleDataTransferObject>> GetReferenceTagStylesAsync(object id)
        {
            if (id == null)
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

            return journalStyle?.ReferenceTagStyles?.ToArray<IReferenceTagStyleDataTransferObject>() ?? Array.Empty<IReferenceTagStyleDataTransferObject>();
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