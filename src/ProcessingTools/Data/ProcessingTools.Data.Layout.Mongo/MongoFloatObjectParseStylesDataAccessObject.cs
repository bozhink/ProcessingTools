// <copyright file="MongoFloatObjectParseStylesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
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
    using ProcessingTools.Data.Models.Contracts.Layout.Styles.Floats;
    using ProcessingTools.Data.Models.Layout.Mongo;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Extensions;
    using ProcessingTools.Models.Contracts.Layout.Styles.Floats;

    /// <summary>
    /// MongoDB implementation of <see cref="IFloatObjectParseStylesDataAccessObject"/>.
    /// </summary>
    public class MongoFloatObjectParseStylesDataAccessObject : MongoDataAccessObjectBase<FloatObjectParseStyle>, IFloatObjectParseStylesDataAccessObject
    {
        private readonly IApplicationContext applicationContext;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoFloatObjectParseStylesDataAccessObject"/> class.
        /// </summary>
        /// <param name="databaseProvider">Instance of <see cref="IMongoDatabaseProvider"/>.</param>
        /// <param name="applicationContext">Application context.</param>
        public MongoFloatObjectParseStylesDataAccessObject(IMongoDatabaseProvider databaseProvider, IApplicationContext applicationContext)
            : base(databaseProvider)
        {
            this.applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IFloatObjectInsertParseStyleModel, FloatObjectParseStyle>();
                c.CreateMap<IFloatObjectUpdateParseStyleModel, FloatObjectParseStyle>();
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
        public async Task<IFloatObjectParseStyleDataModel> GetByIdAsync(object id) => await this.GetDetailsByIdAsync(id).ConfigureAwait(false);

        /// <inheritdoc/>
        public async Task<IFloatObjectDetailsParseStyleDataModel> GetDetailsByIdAsync(object id)
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
        public async Task<IFloatObjectParseStyleDataModel> InsertAsync(IFloatObjectInsertParseStyleModel model)
        {
            if (model == null)
            {
                return null;
            }

            var parseStyle = this.mapper.Map<IFloatObjectInsertParseStyleModel, FloatObjectParseStyle>(model);
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
        public async Task<IFloatObjectParseStyleDataModel[]> SelectAsync(int skip, int take)
        {
            var parseStyles = await this.Collection.Find(Builders<FloatObjectParseStyle>.Filter.Empty)
                .SortBy(p => p.Name)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (parseStyles == null || !parseStyles.Any())
            {
                return Array.Empty<IFloatObjectParseStyleDataModel>();
            }

            return parseStyles.ToArray<IFloatObjectParseStyleDataModel>();
        }

        /// <inheritdoc/>
        public async Task<IFloatObjectDetailsParseStyleDataModel[]> SelectDetailsAsync(int skip, int take)
        {
            var parseStyles = await this.Collection.Find(Builders<FloatObjectParseStyle>.Filter.Empty)
                .SortBy(p => p.Name)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (parseStyles == null || !parseStyles.Any())
            {
                return Array.Empty<IFloatObjectDetailsParseStyleDataModel>();
            }

            return parseStyles.ToArray<IFloatObjectDetailsParseStyleDataModel>();
        }

        /// <inheritdoc/>
        public Task<long> SelectCountAsync()
        {
            return this.Collection.CountDocumentsAsync(Builders<FloatObjectParseStyle>.Filter.Empty);
        }

        /// <inheritdoc/>
        public async Task<IFloatObjectParseStyleDataModel> UpdateAsync(IFloatObjectUpdateParseStyleModel model)
        {
            if (model == null)
            {
                return null;
            }

            Guid objectId = model.Id.ToNewGuid();

            var parseStyle = this.mapper.Map<IFloatObjectUpdateParseStyleModel, FloatObjectParseStyle>(model);
            parseStyle.ModifiedBy = this.applicationContext.UserContext.UserId;
            parseStyle.ModifiedOn = this.applicationContext.DateTimeProvider.Invoke();

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
                IsUpsert = false
            };

            var result = await this.Collection.UpdateOneAsync(filterDefinition, updateDefinition, updateOptions).ConfigureAwait(false);

            if (!result.IsAcknowledged)
            {
                throw new UpdateUnsuccessfulException();
            }

            return parseStyle;
        }

        /// <inheritdoc/>
        public async Task<IIdentifiedStyleDataModel> GetStyleByIdAsync(object id)
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
                    Description = s.Description
                })
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            return style;
        }

        /// <inheritdoc/>
        public async Task<IIdentifiedStyleDataModel[]> GetStylesForSelectAsync()
        {
            var data = await this.Collection.Find(Builders<FloatObjectParseStyle>.Filter.Empty)
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
    }
}
