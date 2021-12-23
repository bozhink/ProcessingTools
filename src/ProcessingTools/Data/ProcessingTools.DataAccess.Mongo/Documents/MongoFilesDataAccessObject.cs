// <copyright file="MongoFilesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DataAccess.Mongo.Documents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using MongoDB.Driver;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts.DataAccess.Documents;
    using ProcessingTools.Contracts.DataAccess.Models.Documents.Files;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Documents.Files;
    using ProcessingTools.Data.Models.Mongo.Documents;
    using ProcessingTools.Data.Mongo;
    using ProcessingTools.Data.Mongo.Abstractions;
    using ProcessingTools.Extensions;

    /// <summary>
    /// MongoDB implementation of <see cref="IFilesDataAccessObject"/>.
    /// </summary>
    public class MongoFilesDataAccessObject : MongoDataAccessObjectBase<File>, IFilesDataAccessObject
    {
        private readonly IApplicationContext applicationContext;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoFilesDataAccessObject"/> class.
        /// </summary>
        /// <param name="databaseProvider">Instance of <see cref="IMongoDatabaseProvider"/>.</param>
        /// <param name="applicationContext">Application context.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        public MongoFilesDataAccessObject(IMongoDatabaseProvider databaseProvider, IApplicationContext applicationContext, IMapper mapper)
            : base(databaseProvider)
        {
            this.applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            this.CollectionSettings = new MongoCollectionSettings
            {
                AssignIdOnInsert = true,
                WriteConcern = new WriteConcern(WriteConcern.WMajority.W),
            };
        }

        /// <inheritdoc/>
        public async Task<object> DeleteAsync(object id)
        {
            if (id is null)
            {
                return null;
            }

            Guid objectId = id.ToNewGuid();

            var result = await this.Collection.DeleteOneAsync(f => f.ObjectId == objectId).ConfigureAwait(false);

            if (!result.IsAcknowledged)
            {
                throw new DeleteUnsuccessfulException();
            }

            return result;
        }

        /// <inheritdoc/>
        public async Task<IFileDataTransferObject> GetByIdAsync(object id) => await this.GetDetailsByIdAsync(id).ConfigureAwait(false);

        /// <inheritdoc/>
        public async Task<IFileDetailsDataTransferObject> GetDetailsByIdAsync(object id)
        {
            if (id is null)
            {
                return null;
            }

            Guid objectId = id.ToNewGuid();

            var file = await this.Collection.Find(f => f.ObjectId == objectId).FirstOrDefaultAsync().ConfigureAwait(false);

            return file;
        }

        /// <inheritdoc/>
        public async Task<IFileDataTransferObject> InsertAsync(IFileInsertModel model)
        {
            if (model is null)
            {
                return null;
            }

            var file = this.mapper.Map<IFileInsertModel, File>(model);
            file.ObjectId = this.applicationContext.GuidProvider.Invoke();
            file.ModifiedBy = this.applicationContext.UserContext.UserId;
            file.ModifiedOn = this.applicationContext.DateTimeProvider.Invoke();
            file.CreatedBy = file.ModifiedBy;
            file.CreatedOn = file.ModifiedOn;
            file.Id = null;

            await this.Collection.InsertOneAsync(file, new InsertOneOptions { BypassDocumentValidation = false }).ConfigureAwait(false);

            return file;
        }

        /// <inheritdoc/>
        public async Task<IList<IFileDataTransferObject>> SelectAsync(int skip, int take)
        {
            var files = await this.Collection.Find(Builders<File>.Filter.Empty)
                .SortBy(f => f.CreatedOn)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (files is null || !files.Any())
            {
                return Array.Empty<IFileDataTransferObject>();
            }

            return files.ToArray<IFileDataTransferObject>();
        }

        /// <inheritdoc/>
        public async Task<IList<IFileDetailsDataTransferObject>> SelectDetailsAsync(int skip, int take)
        {
            var files = await this.Collection.Find(Builders<File>.Filter.Empty)
                .SortBy(f => f.CreatedOn)
                .Skip(skip)
                .Limit(take)
                .ToListAsync()
                .ConfigureAwait(false);

            if (files is null || !files.Any())
            {
                return Array.Empty<IFileDetailsDataTransferObject>();
            }

            return files.ToArray<IFileDetailsDataTransferObject>();
        }

        /// <inheritdoc/>
        public Task<long> SelectCountAsync()
        {
            return this.Collection.CountDocumentsAsync(Builders<File>.Filter.Empty);
        }

        /// <inheritdoc/>
        public async Task<IFileDataTransferObject> UpdateAsync(IFileUpdateModel model)
        {
            if (model is null)
            {
                return null;
            }

            Guid objectId = model.Id.ToNewGuid();

            var file = this.mapper.Map<IFileUpdateModel, File>(model);
            file.ModifiedBy = this.applicationContext.UserContext.UserId;
            file.ModifiedOn = this.applicationContext.DateTimeProvider.Invoke();

            var filterDefinition = new FilterDefinitionBuilder<File>().Eq(m => m.ObjectId, objectId);
            var updateDefinition = new UpdateDefinitionBuilder<File>()
                .Set(m => m.ContentLength, model.ContentLength)
                .Set(m => m.ContentType, model.ContentType)
                .Set(m => m.FileExtension, model.FileExtension)
                .Set(m => m.FileName, model.FileName)
                .Set(m => m.ModifiedBy, file.ModifiedBy)
                .Set(m => m.ModifiedOn, file.ModifiedOn);
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

            return file;
        }
    }
}
