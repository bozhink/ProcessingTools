// <copyright file="MongoJournalStylesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Layout.Mongo
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using MongoDB.Driver;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Common.Mongo;
    using ProcessingTools.Data.Common.Mongo.Contracts;
    using ProcessingTools.Data.Contracts.Layout.Styles;
    using ProcessingTools.Data.Models.Contracts.Layout.Styles;
    using ProcessingTools.Data.Models.Contracts.Layout.Styles.Journals;
    using ProcessingTools.Data.Models.Layout.Mongo;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Extensions;
    using ProcessingTools.Models.Contracts.Layout.Styles.Floats;
    using ProcessingTools.Models.Contracts.Layout.Styles.Journals;
    using ProcessingTools.Models.Contracts.Layout.Styles.References;

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
        public MongoJournalStylesDataAccessObject(IMongoDatabaseProvider databaseProvider, IApplicationContext applicationContext)
            : base(databaseProvider)
        {
            this.applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IJournalInsertStyleModel, JournalStyle>();
                c.CreateMap<IJournalUpdateStyleModel, JournalStyle>();
            });

            this.mapper = mapperConfiguration.CreateMapper();

            this.CollectionSettings = new MongoCollectionSettings
            {
                AssignIdOnInsert = true,
                GuidRepresentation = MongoDB.Bson.GuidRepresentation.Unspecified,
                WriteConcern = new WriteConcern(WriteConcern.WMajority.W)
            };
        }

        /// <inheritdoc/>
        public async Task<IJournalStyleDataModel> GetByIdAsync(object id)
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
        public async Task<IJournalDetailsStyleDataModel> GetDetailsByIdAsync(object id)
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
        public async Task<IJournalStyleDataModel> InsertAsync(IJournalInsertStyleModel model)
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
        public async Task<IJournalStyleDataModel[]> SelectAsync(int skip, int take)
        {
            var journalStyles = await this.Collection.Find(Builders<JournalStyle>.Filter.Empty)
                .SortBy(s => s.Name)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (journalStyles == null || !journalStyles.Any())
            {
                return new IJournalStyleDataModel[] { };
            }

            return journalStyles.ToArray<IJournalStyleDataModel>();
        }

        /// <inheritdoc/>
        public async Task<IJournalDetailsStyleDataModel[]> SelectDetailsAsync(int skip, int take)
        {
            var query = this.GetDetailsLookup(this.Collection.Aggregate())
                .SortBy(s => s.Name)
                .Skip(skip)
                .Limit(take);

            var journalStyles = await query.ToListAsync().ConfigureAwait(false);

            if (journalStyles == null || !journalStyles.Any())
            {
                return new IJournalDetailsStyleDataModel[] { };
            }

            return journalStyles.ToArray<IJournalDetailsStyleDataModel>();
        }

        /// <inheritdoc/>
        public Task<long> SelectCountAsync()
        {
            return this.Collection.CountAsync(Builders<JournalStyle>.Filter.Empty);
        }

        /// <inheritdoc/>
        public async Task<IJournalStyleDataModel> UpdateAsync(IJournalUpdateStyleModel model)
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
                IsUpsert = false
            };

            var result = await this.Collection.UpdateOneAsync(filterDefinition, updateDefinition, updateOptions).ConfigureAwait(false);

            if (!result.IsAcknowledged)
            {
                throw new UpdateUnsuccessfulException();
            }

            return journalStyle;
        }

        /// <inheritdoc/>
        public async Task<IIdentifiedStyleDataModel[]> GetStylesForSelectAsync()
        {
            var data = await this.Collection.Find(Builders<JournalStyle>.Filter.Empty)
                .Project(s => new StyleDataModel
                {
                    Id = s.Id,
                    ObjectId = s.ObjectId,
                    Name = s.Name,
                    Description = s.Description
                })
                .ToListAsync()
                .ConfigureAwait(false);
            return data.ToArray();
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
