// <copyright file="MongoMediatypesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Files.Mongo
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using MongoDB.Driver;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Common.Mongo;
    using ProcessingTools.Data.Common.Mongo.Contracts;
    using ProcessingTools.Data.Contracts.Files;
    using ProcessingTools.Data.Models.Contracts.Files.Mediatypes;
    using ProcessingTools.Data.Models.Files.Mongo;
    using ProcessingTools.Extensions;
    using ProcessingTools.Models.Contracts.Files.Mediatypes;

    /// <summary>
    /// MongoDB implementation of <see cref="IMediatypesDataAccessObject"/>.
    /// </summary>
    public class MongoMediatypesDataAccessObject : MongoDataAccessObjectBase<Mediatype>, IMediatypesDataAccessObject
    {
        private readonly IApplicationContext applicationContext;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoMediatypesDataAccessObject"/> class.
        /// </summary>
        /// <param name="databaseProvider">Instance of <see cref="IMongoDatabaseProvider"/>.</param>
        /// <param name="applicationContext">Application context.</param>
        public MongoMediatypesDataAccessObject(IMongoDatabaseProvider databaseProvider, IApplicationContext applicationContext)
            : base(databaseProvider)
        {
            this.applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IMediatypeInsertModel, Mediatype>();
                c.CreateMap<IMediatypeUpdateModel, Mediatype>();
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
        public async Task<IMediatypeDataModel> InsertAsync(IMediatypeInsertModel model)
        {
            if (model == null)
            {
                return null;
            }

            var mediatype = this.mapper.Map<IMediatypeInsertModel, Mediatype>(model);
            mediatype.ObjectId = this.applicationContext.GuidProvider.Invoke();
            mediatype.ModifiedBy = this.applicationContext.UserContext.UserId;
            mediatype.ModifiedOn = this.applicationContext.DateTimeProvider.Invoke();
            mediatype.CreatedBy = mediatype.ModifiedBy;
            mediatype.CreatedOn = mediatype.ModifiedOn;
            mediatype.Id = null;

            await this.Collection.InsertOneAsync(mediatype, new InsertOneOptions { BypassDocumentValidation = false }).ConfigureAwait(false);

            return mediatype;
        }

        /// <inheritdoc/>
        public async Task<IMediatypeDataModel> UpdateAsync(IMediatypeUpdateModel model)
        {
            if (model == null)
            {
                return null;
            }

            Guid objectId = model.Id.ToNewGuid();

            var mediatype = this.mapper.Map<IMediatypeUpdateModel, Mediatype>(model);
            mediatype.ModifiedBy = this.applicationContext.UserContext.UserId;
            mediatype.ModifiedOn = this.applicationContext.DateTimeProvider.Invoke();

            var filterDefinition = new FilterDefinitionBuilder<Mediatype>().Eq(m => m.ObjectId, objectId);
            var updateDefinition = new UpdateDefinitionBuilder<Mediatype>()
                .Set(m => m.Extension, model.Extension)
                .Set(m => m.MimeType, model.MimeType)
                .Set(m => m.MimeSubtype, model.MimeSubtype)
                .Set(m => m.Description, model.Description)
                .Set(m => m.ModifiedBy, mediatype.ModifiedBy)
                .Set(m => m.ModifiedOn, mediatype.ModifiedOn);
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

            return mediatype;
        }

        /// <inheritdoc/>
        public async Task<object> DeleteAsync(object id)
        {
            if (id == null)
            {
                return null;
            }

            Guid objectId = id.ToNewGuid();

            var result = await this.Collection.DeleteOneAsync(a => a.ObjectId == objectId).ConfigureAwait(false);

            if (!result.IsAcknowledged)
            {
                throw new DeleteUnsuccessfulException();
            }

            return result;
        }

        /// <inheritdoc/>
        public async Task<IMediatypeDataModel> GetByIdAsync(object id) => await this.GetDetailsByIdAsync(id).ConfigureAwait(false);

        /// <inheritdoc/>
        public async Task<IMediatypeDetailsDataModel> GetDetailsByIdAsync(object id)
        {
            if (id == null)
            {
                return null;
            }

            Guid objectId = id.ToNewGuid();

            var mediatype = await this.Collection.Find(a => a.ObjectId == objectId).FirstOrDefaultAsync().ConfigureAwait(false);

            return mediatype;
        }

        /// <inheritdoc/>
        public async Task<IMediatypeMetaModel> GetMediatypeByExtensionAsync(string extension)
        {
            if (string.IsNullOrWhiteSpace(extension?.TrimStart('.')))
            {
                return null;
            }

            string extensionCleaned = ("." + extension.TrimStart('.').Trim(' ')).ToLowerInvariant();

            var data = await this.Collection.Find(m => m.Extension == extensionCleaned).FirstOrDefaultAsync().ConfigureAwait(false);

            return data;
        }

        /// <inheritdoc/>
        public async Task<IMediatypeMetaModel[]> GetMediatypesByExtensionAsync(string extension)
        {
            if (string.IsNullOrWhiteSpace(extension?.TrimStart('.')))
            {
                return Array.Empty<IMediatypeMetaModel>();
            }

            string extensionCleaned = ("." + extension.TrimStart('.').Trim(' ')).ToLowerInvariant();

            var data = await this.Collection.Find(m => m.Extension == extensionCleaned).ToListAsync().ConfigureAwait(false);

            return data.ToArray<IMediatypeMetaModel>();
        }

        /// <inheritdoc/>
        public async Task<string[]> GetMimeTypesAsync()
        {
            var query = this.Collection.Aggregate().Project(m => new { m.MimeType }).Group(m => m.MimeType, g => new { g.Key });

            var data = await query.ToListAsync().ConfigureAwait(false);

            return data.Select(g => g.Key).Distinct().ToArray();
        }

        /// <inheritdoc/>
        public async Task<string[]> GetMimeSubtypesAsync()
        {
            var query = this.Collection.Aggregate().Project(m => new { m.MimeSubtype }).Group(m => m.MimeSubtype, g => new { g.Key });

            var data = await query.ToListAsync().ConfigureAwait(false);

            return data.Select(g => g.Key).Distinct().ToArray();
        }

        /// <inheritdoc/>
        public Task<long> SelectCountAsync()
        {
            return this.Collection.CountDocumentsAsync(a => true);
        }

        /// <inheritdoc/>
        public async Task<IMediatypeDataModel[]> SelectAsync(int skip, int take)
        {
            var mediatypes = await this.Collection.Find(a => true)
                .SortByDescending(a => a.CreatedOn)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (mediatypes == null || !mediatypes.Any())
            {
                return Array.Empty<IMediatypeDataModel>();
            }

            return mediatypes.ToArray<IMediatypeDataModel>();
        }

        /// <inheritdoc/>
        public async Task<IMediatypeDetailsDataModel[]> SelectDetailsAsync(int skip, int take)
        {
            var mediatypes = await this.Collection.Find(a => true)
               .SortByDescending(a => a.CreatedOn)
               .Skip(skip)
               .Limit(take)
               .ToListAsync()
               .ConfigureAwait(false);

            if (mediatypes == null || !mediatypes.Any())
            {
                return Array.Empty<IMediatypeDetailsDataModel>();
            }

            return mediatypes.ToArray<IMediatypeDetailsDataModel>();
        }
    }
}
