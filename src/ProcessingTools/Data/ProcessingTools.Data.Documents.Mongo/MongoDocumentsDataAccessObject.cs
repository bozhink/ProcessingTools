// <copyright file="MongoDocumentsDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Documents.Mongo
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
    using ProcessingTools.Data.Contracts.Documents;
    using ProcessingTools.Data.Models.Contracts.Documents.Documents;
    using ProcessingTools.Data.Models.Documents.Mongo;
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
                c.CreateMap<IDocumentFileModel, File>();
                c.CreateMap<IDocumentInsertModel, Document>();
                c.CreateMap<IDocumentUpdateModel, Document>();
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

            if (document.File != null)
            {
                var file = this.mapper.Map<IDocumentFileModel, File>(document.File);
                file.OriginalContentType = file.ContentType;
                file.OriginalContentLength = file.ContentLength;
                file.OriginalFileExtension = file.FileExtension;
                file.OriginalFileName = file.FileName;
                file.SystemFileName = null;
                file.ObjectId = this.applicationContext.GuidProvider.Invoke();
                file.CreatedBy = document.CreatedBy;
                file.CreatedOn = document.CreatedOn;
                file.ModifiedBy = document.ModifiedBy;
                file.ModifiedOn = document.ModifiedOn;

                document.FileId = file.ObjectId.ToString();
                document.File = file;
            }

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
                return Array.Empty<IDocumentDataModel>();
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
                return Array.Empty<IDocumentDetailsDataModel>();
            }

            return documents.ToArray<IDocumentDetailsDataModel>();
        }

        /// <inheritdoc/>
        public Task<long> SelectCountAsync()
        {
            return this.Collection.CountDocumentsAsync(Builders<Document>.Filter.Empty);
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
                .Set(m => m.ArticleId, model.ArticleId)
                .Set(m => m.Description, model.Description)
                .Set(m => m.File.ContentType, model.File.ContentType)
                .Set(m => m.File.ContentLength, model.File.ContentLength)
                .Set(m => m.File.FileExtension, model.File.FileExtension)
                .Set(m => m.File.FileName, model.File.FileName)
                .Set(m => m.ModifiedBy, document.ModifiedBy)
                .Set(m => m.ModifiedOn, document.ModifiedOn);
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
                .Find(c => c.DocumentId == objectId)
                .Project(c => c.Content)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            return result;
        }

        /// <inheritdoc/>
        public async Task<IDocumentDataModel[]> GetArticleDocumentsAsync(string articleId)
        {
            if (string.IsNullOrWhiteSpace(articleId))
            {
                return Array.Empty<IDocumentDataModel>();
            }

            var result = await this.Collection
                .Find(d => d.ArticleId == articleId)
                .Project(d => new Document
                {
                    Id = d.Id,
                    ObjectId = d.ObjectId,
                    ArticleId = d.ArticleId,
                    Description = d.Description,
                    FileId = d.FileId,
                    File = new File
                    {
                        FileName = d.File.FileName
                    },
                    IsFinal = d.IsFinal,
                    CreatedBy = d.CreatedBy,
                    CreatedOn = d.CreatedOn,
                    ModifiedBy = d.ModifiedBy,
                    ModifiedOn = d.ModifiedOn
                })
                .ToListAsync().ConfigureAwait(false);

            return result.ToArray<IDocumentDataModel>();
        }

        /// <inheritdoc/>
        public async Task<IDocumentArticleDataModel> GetDocumentArticleAsync(string articleId)
        {
            if (string.IsNullOrWhiteSpace(articleId))
            {
                return null;
            }

            var article = await this.GetCollection<Article>()
                .Find(a => a.ObjectId == articleId.ToNewGuid())
                .Project(a => new DocumentArticleDataModel
                {
                    ArticleId = a.ObjectId.ToString(),
                    ArticleTitle = a.Title,
                    JournalId = a.JournalId
                })
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if (article != null && string.IsNullOrWhiteSpace(article.JournalName))
            {
                var journal = await this.GetCollection<Journal>()
                    .Find(j => j.ObjectId == article.JournalId.ToNewGuid())
                    .Project(j => new
                    {
                        JournalId = j.ObjectId.ToString(),
                        JournalName = j.Name
                    })
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);

                article.JournalName = journal.JournalName;
            }

            return article;
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
                .Set(m => m.ContentType, ProcessingTools.Common.Constants.ContentTypes.Xml)
                .Set(m => m.Content, content)
                .Set(m => m.ModifiedBy, this.applicationContext.UserContext.UserId)
                .Set(m => m.ModifiedOn, this.applicationContext.DateTimeProvider.Invoke());

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

        /// <inheritdoc/>
        public async Task<object> SetAsFinalAsync(object id, string articleId)
        {
            if (id == null || string.IsNullOrWhiteSpace(articleId))
            {
                return null;
            }

            var objectId = id.ToNewGuid();

            var updateOptions = new UpdateOptions
            {
                BypassDocumentValidation = false,
                IsUpsert = false
            };

            var resultMany = await this.Collection
                .UpdateManyAsync(
                    Builders<Document>.Filter
                        .And(
                            Builders<Document>.Filter.Eq(m => m.ArticleId, articleId),
                            Builders<Document>.Filter.Ne(m => m.ObjectId, objectId)),
                    Builders<Document>.Update
                        .Set(m => m.IsFinal, false)
                        .Set(m => m.ModifiedBy, this.applicationContext.UserContext.UserId)
                        .Set(m => m.ModifiedOn, this.applicationContext.DateTimeProvider.Invoke()),
                    updateOptions)
                .ConfigureAwait(false);

            if (!resultMany.IsAcknowledged)
            {
                throw new UpdateUnsuccessfulException("Update all documents as not final failed.");
            }

            var resultOne = await this.Collection
                .UpdateOneAsync(
                    Builders<Document>.Filter
                        .And(
                            Builders<Document>.Filter.Eq(m => m.ArticleId, articleId),
                            Builders<Document>.Filter.Eq(m => m.ObjectId, objectId)),
                    Builders<Document>.Update
                        .Set(m => m.IsFinal, true)
                        .Set(m => m.ModifiedBy, this.applicationContext.UserContext.UserId)
                        .Set(m => m.ModifiedOn, this.applicationContext.DateTimeProvider.Invoke()),
                    updateOptions)
                .ConfigureAwait(false);

            if (!resultOne.IsAcknowledged || (resultOne.IsModifiedCountAvailable && resultOne.ModifiedCount != 1))
            {
                throw new UpdateUnsuccessfulException("Update single document as final failed.");
            }

            return resultOne;
        }
    }
}
