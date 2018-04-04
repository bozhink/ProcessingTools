// <copyright file="MongoDocumentsDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Documents.Mongo
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using MongoDB.Driver;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Common.Mongo;
    using ProcessingTools.Data.Common.Mongo.Contracts;
    using ProcessingTools.Data.Contracts.Documents;
    using ProcessingTools.Data.Models.Contracts.Documents.Documents;
    using ProcessingTools.Data.Models.Documents.Mongo;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Extensions;
    using ProcessingTools.Models.Contracts.Documents.Documents;

    /// <summary>
    /// MongoDB implementation of <see cref="IDocumentsDataAccessObject"/>.
    /// </summary>
    public class MongoDocumentsDataAccessObject : MongoDataAccessObjectBase<Document>, IDocumentsDataAccessObject
    {
        private readonly IApplicationContext applicationContext;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDocumentsDataAccessObject"/> class.
        /// </summary>
        /// <param name="databaseProvider">Instance of <see cref="IMongoDatabaseProvider"/>.</param>
        /// <param name="applicationContext">Application context.</param>
        public MongoDocumentsDataAccessObject(IMongoDatabaseProvider databaseProvider, IApplicationContext applicationContext)
            : base(databaseProvider)
        {
            this.applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IDocumentInsertModel, Document>();
                c.CreateMap<IDocumentUpdateModel, Document>();
                c.CreateMap<IDocumentFileModel, File>();
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
        public async Task<object> DeleteAsync(object id)
        {
            if (id == null)
            {
                return null;
            }

            Guid objectId = id.ToNewGuid();

            var result = await this.Collection.DeleteOneAsync(d => d.ObjectId == objectId).ConfigureAwait(false);

            if (!result.IsAcknowledged)
            {
                throw new DeleteUnsuccessfulException("Delete document is not acknowledged.");
            }

            var resultContent = await this.GetCollection<DocumentContent>().DeleteManyAsync(c => c.DocumentId == objectId.ToString()).ConfigureAwait(false);
            if (!resultContent.IsAcknowledged)
            {
                throw new DeleteUnsuccessfulException("Delete document content is not acknowledged.");
            }

            return result;
        }

        /// <inheritdoc/>
        public async Task<IDocumentDataModel> GetByIdAsync(object id) => await this.GetDetailsByIdAsync(id).ConfigureAwait(false);

        /// <inheritdoc/>
        public async Task<IDocumentDetailsDataModel> GetDetailsByIdAsync(object id)
        {
            if (id == null)
            {
                return null;
            }

            Guid objectId = id.ToNewGuid();

            var document = await this.Collection.Find(d => d.ObjectId == objectId).FirstOrDefaultAsync().ConfigureAwait(false);

            return document;
        }

        /// <inheritdoc/>
        public async Task<IDocumentDataModel> InsertAsync(IDocumentInsertModel model)
        {
            if (model == null)
            {
                return null;
            }

            var document = this.mapper.Map<IDocumentInsertModel, Document>(model);
            document.IsFinal = false;
            document.ObjectId = this.applicationContext.GuidProvider.Invoke();
            document.ModifiedBy = this.applicationContext.UserContext.UserId;
            document.ModifiedOn = this.applicationContext.DateTimeProvider.Invoke();
            document.CreatedBy = document.ModifiedBy;
            document.CreatedOn = document.ModifiedOn;
            document.Id = null;

            await this.Collection.InsertOneAsync(document, new InsertOneOptions { BypassDocumentValidation = false }).ConfigureAwait(false);

            return document;
        }

        /// <inheritdoc/>
        public async Task<IDocumentDataModel[]> SelectAsync(int skip, int take)
        {
            var documents = await this.Collection.Find(Builders<Document>.Filter.Empty)
               .SortByDescending(d => d.CreatedOn)
               .Skip(skip)
               .Limit(take)
               .ToListAsync()
               .ConfigureAwait(false);

            if (documents == null || !documents.Any())
            {
                return new IDocumentDataModel[] { };
            }

            return documents.ToArray<IDocumentDataModel>();
        }

        /// <inheritdoc/>
        public async Task<IDocumentDetailsDataModel[]> SelectDetailsAsync(int skip, int take)
        {
            var documents = await this.Collection.Find(Builders<Document>.Filter.Empty)
               .SortByDescending(d => d.CreatedOn)
               .Skip(skip)
               .Limit(take)
               .ToListAsync()
               .ConfigureAwait(false);

            if (documents == null || !documents.Any())
            {
                return new IDocumentDetailsDataModel[] { };
            }

            return documents.ToArray<IDocumentDetailsDataModel>();
        }

        /// <inheritdoc/>
        public Task<long> SelectCountAsync()
        {
            return this.Collection.CountAsync(Builders<Document>.Filter.Empty);
        }

        /// <inheritdoc/>
        public async Task<IDocumentDataModel> UpdateAsync(IDocumentUpdateModel model)
        {
            if (model == null)
            {
                return null;
            }

            Guid objectId = model.Id.ToNewGuid();

            var document = this.mapper.Map<IDocumentUpdateModel, Document>(model);
            document.ModifiedBy = this.applicationContext.UserContext.UserId;
            document.ModifiedOn = this.applicationContext.DateTimeProvider.Invoke();

            var filterDefinition = new FilterDefinitionBuilder<Document>().Eq(m => m.ObjectId, objectId);
            var updateDefinition = new UpdateDefinitionBuilder<Document>()
                .Set(d => d.ArticleId, model.ArticleId)
                .Set(d => d.Description, model.Description)
                .Set(d => d.ModifiedBy, document.ModifiedBy)
                .Set(d => d.ModifiedOn, document.ModifiedOn);
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

            return document;
        }

        /// <inheritdoc/>
        public async Task<string> GetDocumentContentAsync(object id)
        {
            if (id == null)
            {
                return null;
            }

            string objectId = id.ToNewGuid().ToString();

            var result = await this.GetCollection<DocumentContent>()
                .Find(d => d.DocumentId == objectId)
                .Project(d => d.Content)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            return result;
        }

        /// <inheritdoc/>
        public async Task<long> SetDocumentContentAsync(object id, string content)
        {
            if (id == null)
            {
                return 0L;
            }

            string objectId = id.ToNewGuid().ToString();

            var filterDefinition = new FilterDefinitionBuilder<DocumentContent>().Eq(m => m.DocumentId, objectId);
            var updateDefinition = new UpdateDefinitionBuilder<DocumentContent>()
                .Set(d => d.ContentType, ProcessingTools.Constants.ContentTypes.Xml)
                .Set(d => d.Content, content)
                .Set(d => d.ModifiedBy, this.applicationContext.UserContext.UserId)
                .Set(d => d.ModifiedOn, this.applicationContext.DateTimeProvider.Invoke());

            var updateOptions = new UpdateOptions
            {
                BypassDocumentValidation = false,
                IsUpsert = true
            };

            var result = await this.GetCollection<DocumentContent>()
                .UpdateOneAsync(filterDefinition, updateDefinition, updateOptions)
                .ConfigureAwait(false);

            if (!result.IsAcknowledged)
            {
                throw new UpdateUnsuccessfulException();
            }

            return content?.Length ?? 0L;
        }
    }
}
