// <copyright file="MongoMediatypesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DataAccess.Mongo.Files
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using MongoDB.Driver;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts.DataAccess.Files;
    using ProcessingTools.Contracts.DataAccess.Models.Files.Mediatypes;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Files.Mediatypes;
    using ProcessingTools.Data.Models.Mongo.Files;
    using ProcessingTools.Extensions;

    /// <summary>
    /// MongoDB implementation of <see cref="IMediatypesDataAccessObject"/>.
    /// </summary>
    public class MongoMediatypesDataAccessObject : IMediatypesDataAccessObject
    {
        private readonly IMongoCollection<Mediatype> collection;
        private readonly IApplicationContext applicationContext;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoMediatypesDataAccessObject"/> class.
        /// </summary>
        /// <param name="collection">Instance of <see cref="IMongoCollection{Mediatype}"/>.</param>
        /// <param name="applicationContext">Instance of <see cref="IApplicationContext"/>.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        public MongoMediatypesDataAccessObject(IMongoCollection<Mediatype> collection, IApplicationContext applicationContext, IMapper mapper)
        {
            this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
            this.applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <inheritdoc/>
        public async Task<IMediatypeDataTransferObject> InsertAsync(IMediatypeInsertModel model)
        {
            if (model is null)
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

            await this.collection.InsertOneAsync(mediatype, new InsertOneOptions { BypassDocumentValidation = false }).ConfigureAwait(false);

            return mediatype;
        }

        /// <inheritdoc/>
        public async Task<IMediatypeDataTransferObject> UpdateAsync(IMediatypeUpdateModel model)
        {
            if (model is null)
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
                IsUpsert = false,
            };

            var result = await this.collection.UpdateOneAsync(filterDefinition, updateDefinition, updateOptions).ConfigureAwait(false);

            if (!result.IsAcknowledged)
            {
                throw new UpdateUnsuccessfulException();
            }

            return mediatype;
        }

        /// <inheritdoc/>
        public async Task<object> DeleteAsync(object id)
        {
            if (id is null)
            {
                return null;
            }

            Guid objectId = id.ToNewGuid();

            var result = await this.collection.DeleteOneAsync(a => a.ObjectId == objectId).ConfigureAwait(false);

            if (!result.IsAcknowledged)
            {
                throw new DeleteUnsuccessfulException();
            }

            return result;
        }

        /// <inheritdoc/>
        public async Task<IMediatypeDataTransferObject> GetByIdAsync(object id) => await this.GetDetailsByIdAsync(id).ConfigureAwait(false);

        /// <inheritdoc/>
        public async Task<IMediatypeDetailsDataTransferObject> GetDetailsByIdAsync(object id)
        {
            if (id is null)
            {
                return null;
            }

            Guid objectId = id.ToNewGuid();

            var mediatype = await this.collection.Find(a => a.ObjectId == objectId).FirstOrDefaultAsync().ConfigureAwait(false);

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

            var data = await this.collection.Find(m => m.Extension == extensionCleaned).FirstOrDefaultAsync().ConfigureAwait(false);

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

            var data = await this.collection.Find(m => m.Extension == extensionCleaned).ToListAsync().ConfigureAwait(false);

            return data.ToArray<IMediatypeMetaModel>();
        }

        /// <inheritdoc/>
        public async Task<string[]> GetMimeTypesAsync()
        {
            var query = this.collection.Aggregate().Project(m => new { m.MimeType }).Group(m => m.MimeType, g => new { g.Key });

            var data = await query.ToListAsync().ConfigureAwait(false);

            return data.Select(g => g.Key).Distinct().ToArray();
        }

        /// <inheritdoc/>
        public async Task<string[]> GetMimeSubtypesAsync()
        {
            var query = this.collection.Aggregate().Project(m => new { m.MimeSubtype }).Group(m => m.MimeSubtype, g => new { g.Key });

            var data = await query.ToListAsync().ConfigureAwait(false);

            return data.Select(g => g.Key).Distinct().ToArray();
        }

        /// <inheritdoc/>
        public Task<long> SelectCountAsync()
        {
            return this.collection.CountDocumentsAsync(a => true);
        }

        /// <inheritdoc/>
        public async Task<IList<IMediatypeDataTransferObject>> SelectAsync(int skip, int take)
        {
            var mediatypes = await this.collection.Find(a => true)
                .SortByDescending(a => a.CreatedOn)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (mediatypes is null || !mediatypes.Any())
            {
                return Array.Empty<IMediatypeDataTransferObject>();
            }

            return mediatypes.ToArray<IMediatypeDataTransferObject>();
        }

        /// <inheritdoc/>
        public async Task<IList<IMediatypeDetailsDataTransferObject>> SelectDetailsAsync(int skip, int take)
        {
            var mediatypes = await this.collection.Find(a => true)
               .SortByDescending(a => a.CreatedOn)
               .Skip(skip)
               .Limit(take)
               .ToListAsync()
               .ConfigureAwait(false);

            if (mediatypes is null || !mediatypes.Any())
            {
                return Array.Empty<IMediatypeDetailsDataTransferObject>();
            }

            return mediatypes.ToArray<IMediatypeDetailsDataTransferObject>();
        }

        /// <inheritdoc/>
        public Task<long> SaveChangesAsync() => Task.FromResult(-1L);
    }
}
