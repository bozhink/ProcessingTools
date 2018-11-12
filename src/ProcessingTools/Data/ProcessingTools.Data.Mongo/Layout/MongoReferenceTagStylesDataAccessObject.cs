// <copyright file="MongoReferenceTagStylesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Mongo.Layout
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Abstractions;
    using AutoMapper;
    using Contracts.Layout.Styles;
    using Extensions;
    using Models.Contracts.Layout.Styles;
    using Models.Contracts.Layout.Styles.References;
    using Models.Mongo.Layout;
    using MongoDB.Driver;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts;
    using ProcessingTools.Models.Contracts.Layout.Styles.References;

    /// <summary>
    /// MongoDB implementation of <see cref="IReferenceTagStylesDataAccessObject"/>.
    /// </summary>
    public class MongoReferenceTagStylesDataAccessObject : MongoDataAccessObjectBase<ReferenceTagStyle>, IReferenceTagStylesDataAccessObject
    {
        private readonly IApplicationContext applicationContext;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoReferenceTagStylesDataAccessObject"/> class.
        /// </summary>
        /// <param name="databaseProvider">Instance of <see cref="IMongoDatabaseProvider"/>.</param>
        /// <param name="applicationContext">Application context.</param>
        public MongoReferenceTagStylesDataAccessObject(IMongoDatabaseProvider databaseProvider, IApplicationContext applicationContext)
            : base(databaseProvider)
        {
            this.applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IReferenceInsertTagStyleModel, ReferenceTagStyle>();
                c.CreateMap<IReferenceUpdateTagStyleModel, ReferenceTagStyle>();
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
        public async Task<IReferenceTagStyleDataModel> GetByIdAsync(object id) => await this.GetDetailsByIdAsync(id).ConfigureAwait(false);

        /// <inheritdoc/>
        public async Task<IReferenceDetailsTagStyleDataModel> GetDetailsByIdAsync(object id)
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
        public async Task<IReferenceTagStyleDataModel> InsertAsync(IReferenceInsertTagStyleModel model)
        {
            if (model == null)
            {
                return null;
            }

            var tagStyle = this.mapper.Map<IReferenceInsertTagStyleModel, ReferenceTagStyle>(model);
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
        public async Task<IReferenceTagStyleDataModel[]> SelectAsync(int skip, int take)
        {
            var tagStyles = await this.Collection.Find(Builders<ReferenceTagStyle>.Filter.Empty)
                .SortBy(p => p.Name)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (tagStyles == null || !tagStyles.Any())
            {
                return Array.Empty<IReferenceTagStyleDataModel>();
            }

            return tagStyles.ToArray<IReferenceTagStyleDataModel>();
        }

        /// <inheritdoc/>
        public async Task<IReferenceDetailsTagStyleDataModel[]> SelectDetailsAsync(int skip, int take)
        {
            var tagStyles = await this.Collection.Find(Builders<ReferenceTagStyle>.Filter.Empty)
                .SortBy(p => p.Name)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (tagStyles == null || !tagStyles.Any())
            {
                return Array.Empty<IReferenceDetailsTagStyleDataModel>();
            }

            return tagStyles.ToArray<IReferenceDetailsTagStyleDataModel>();
        }

        /// <inheritdoc/>
        public Task<long> SelectCountAsync()
        {
            return this.Collection.CountDocumentsAsync(Builders<ReferenceTagStyle>.Filter.Empty);
        }

        /// <inheritdoc/>
        public async Task<IReferenceTagStyleDataModel> UpdateAsync(IReferenceUpdateTagStyleModel model)
        {
            if (model == null)
            {
                return null;
            }

            Guid objectId = model.Id.ToNewGuid();

            var tagStyle = this.mapper.Map<IReferenceUpdateTagStyleModel, ReferenceTagStyle>(model);
            tagStyle.ModifiedBy = this.applicationContext.UserContext.UserId;
            tagStyle.ModifiedOn = this.applicationContext.DateTimeProvider.Invoke();

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
                IsUpsert = false
            };

            var result = await this.Collection.UpdateOneAsync(filterDefinition, updateDefinition, updateOptions).ConfigureAwait(false);

            if (!result.IsAcknowledged)
            {
                throw new UpdateUnsuccessfulException();
            }

            return tagStyle;
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
            var data = await this.Collection.Find(Builders<ReferenceTagStyle>.Filter.Empty)
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
