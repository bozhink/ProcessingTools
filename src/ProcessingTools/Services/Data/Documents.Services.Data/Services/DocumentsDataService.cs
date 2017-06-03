namespace ProcessingTools.Documents.Services.Data.Services
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Constants;
    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Constants.Data.Documents;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Documents.Data.Entity.Contracts.Repositories;
    using ProcessingTools.Documents.Data.Entity.Models;
    using ProcessingTools.Documents.Services.Data.Contracts;
    using ProcessingTools.Documents.Services.Data.Contracts.Models;
    using ProcessingTools.Documents.Services.Data.Models;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Common.Extensions.Linq;
    using ProcessingTools.FileSystem.Contracts;

    public class DocumentsDataService : IDocumentsDataService
    {
        private readonly IDocumentsRepositoryProvider<Document> repositoryProvider;
        private readonly IXmlFileReaderWriter xmlFileReaderWriter;

        public DocumentsDataService(IDocumentsRepositoryProvider<Document> repositoryProvider, IXmlFileReaderWriter xmlFileReaderWriter)
        {
            this.repositoryProvider = repositoryProvider ?? throw new ArgumentNullException(nameof(repositoryProvider));
            this.xmlFileReaderWriter = xmlFileReaderWriter ?? throw new ArgumentNullException(nameof(xmlFileReaderWriter));
        }

        // TODO: ConfigurationManager
        private string DataDirectory
        {
            get
            {
                string path = ConfigurationManager.AppSettings[AppSettingsKeys.AppDataDirectoryName];
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return path;
            }
        }

        public async Task<IEnumerable<IDocumentServiceModel>> All(object userId, object articleId, int pageNumber, int itemsPerPage)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (articleId == null)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            if (pageNumber < 0)
            {
                throw new InvalidPageNumberException();
            }

            if (1 > itemsPerPage || itemsPerPage > PagingConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException();
            }

            var repository = this.repositoryProvider.Create();

            var documents = await repository.Query
                .Where(d => d.CreatedByUser == userId.ToString())
                //// TODO // .Where(d => d.Article.Id.ToString() == articleId.ToString())
                .OrderByDescending(d => d.DateModified)
                .Skip(pageNumber * itemsPerPage)
                .Take(itemsPerPage)
                .Select(d => new DocumentServiceModel
                {
                    Id = d.Id.ToString(),
                    FileName = d.FileName,
                    FileExtension = d.FileExtension,
                    Comment = d.Comment,
                    ContentType = d.ContentType,
                    ContentLength = d.ContentLength,
                    DateCreated = d.DateCreated,
                    DateModified = d.DateModified
                })
                .ToListAsync();

            repository.TryDispose();

            return documents.AsQueryable();
        }

        public async Task<long> Count(object userId, object articleId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (articleId == null)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            var repository = this.repositoryProvider.Create();

            long count = await repository.Query
                .Where(d => d.CreatedByUser == userId.ToString())
                //// TODO // .Where(d => d.Article.Id.ToString() == articleId.ToString())
                .LongCountAsync();

            repository.TryDispose();

            return count;
        }

        public async Task<object> Create(object userId, object articleId, IDocumentServiceModel document, Stream inputStream)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (articleId == null)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (inputStream == null)
            {
                throw new ArgumentNullException(nameof(inputStream));
            }

            string path = await this.xmlFileReaderWriter.GetNewFilePath(document.FileName, this.DataDirectory, ValidationConstants.MaximalLengthOfFullFileName);

            var entity = new Document
            {
                FileName = document.FileName,
                OriginalFileName = document.FileName,

                ContentLength = document.ContentLength,
                OriginalContentLength = document.ContentLength,

                ContentType = document.ContentType,
                OriginalContentType = document.ContentType,

                FileExtension = document.FileExtension,
                OriginalFileExtension = document.FileExtension,

                Comment = document.Comment,

                CreatedByUser = userId.ToString(),
                ModifiedByUser = userId.ToString()
            };

            var repository = this.repositoryProvider.Create();

            entity.FilePath = Path.GetFileNameWithoutExtension(path);
            entity.ContentLength = await this.xmlFileReaderWriter.Write(inputStream, entity.FilePath, this.DataDirectory);

            await repository.Add(entity);
            await repository.SaveChangesAsync();

            repository.TryDispose();

            return entity.ContentLength;
        }

        public async Task<object> Delete(object userId, object articleId, object documentId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (articleId == null)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            if (documentId == null)
            {
                throw new ArgumentNullException(nameof(documentId));
            }

            var repository = this.repositoryProvider.Create();

            var entity = await this.GetEntity(userId, articleId, documentId, repository);

            await this.xmlFileReaderWriter.Delete(entity.FilePath, this.DataDirectory);

            await repository.Delete(entity.Id);
            await repository.SaveChangesAsync();

            repository.TryDispose();

            return documentId;
        }

        public async Task<object> DeleteAll(object userId, object articleId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (articleId == null)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            var repository = this.repositoryProvider.Create();

            var entities = repository.Query
                .Where(d => d.CreatedByUser == userId.ToString())
                //// TODO // .Where(d => d.Article.Id.ToString() == articleId.ToString())
                .AsEnumerable();

            foreach (var entity in entities)
            {
                await this.xmlFileReaderWriter.Delete(entity.FilePath, this.DataDirectory);
                await repository.Delete(entity.Id);
            }

            var result = await repository.SaveChangesAsync();

            repository.TryDispose();

            return result;
        }

        public async Task<IDocumentServiceModel> Get(object userId, object articleId, object documentId)
        {
            var entity = await this.GetDocument(userId, articleId, documentId);
            return new DocumentServiceModel
            {
                Id = entity.Id.ToString(),
                FileName = entity.FileName,
                FileExtension = entity.FileExtension.Trim('.'),
                ContentLength = entity.ContentLength,
                ContentType = entity.ContentType,
                Comment = entity.Comment,
                DateCreated = entity.DateCreated,
                DateModified = entity.DateModified
            };
        }

        public async Task<XmlReader> GetReader(object userId, object articleId, object documentId)
        {
            var entity = await this.GetDocument(userId, articleId, documentId);
            return this.xmlFileReaderWriter.GetXmlReader(entity.FilePath, this.DataDirectory);
        }

        public async Task<Stream> GetStream(object userId, object articleId, object documentId)
        {
            var entity = await this.GetDocument(userId, articleId, documentId);
            return this.xmlFileReaderWriter.ReadToStream(entity.FilePath, this.DataDirectory);
        }

        public async Task<object> UpdateContent(object userId, object articleId, IDocumentServiceModel document, string content)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (articleId == null)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var repository = this.repositoryProvider.Create();

            var entity = await this.GetEntity(userId, articleId, document.Id, repository);

            using (var stream = new MemoryStream(Defaults.Encoding.GetBytes(content)))
            {
                entity.ContentLength = await this.xmlFileReaderWriter.Write(stream, entity.FilePath, this.DataDirectory);
                entity.ModifiedByUser = userId.ToString();
                entity.DateModified = DateTime.UtcNow;
                entity.ContentType = document.ContentType;
                stream.Close();
            }

            await repository.Update(entity: entity);
            await repository.SaveChangesAsync();

            repository.TryDispose();

            return entity.ContentLength;
        }

        public async Task<object> UpdateMeta(object userId, object articleId, IDocumentServiceModel document)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (articleId == null)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var repository = this.repositoryProvider.Create();

            var entity = await this.GetEntity(userId, articleId, document.Id, repository);

            entity.Comment = document.Comment;
            entity.ContentType = document.ContentType;
            entity.FileExtension = document.FileExtension;
            entity.FileName = document.FileName;
            entity.ModifiedByUser = userId.ToString();
            entity.DateModified = DateTime.UtcNow;

            await repository.Update(entity: entity);
            await repository.SaveChangesAsync();

            repository.TryDispose();

            return entity.ContentLength;
        }

        private async Task<Document> GetDocument(object userId, object articleId, object documentId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (articleId == null)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            if (documentId == null)
            {
                throw new ArgumentNullException(nameof(documentId));
            }

            var repository = this.repositoryProvider.Create();

            var entity = await this.GetEntity(userId, articleId, documentId, repository);

            repository.TryDispose();

            return entity;
        }

        private async Task<Document> GetEntity(object userId, object articleId, object documentId, ICrudRepository<Document> repository)
        {
            var entity = await repository.Query
                .Where(d => d.CreatedByUser == userId.ToString())
                //// TODO: // .Where(d => d.Article.Id.ToString() == articleId.ToString())
                .FirstOrDefaultAsync(d => d.Id.ToString() == documentId.ToString());

            if (entity == null)
            {
                repository.TryDispose();
                throw new EntityNotFoundException();
            }

            return entity;
        }
    }
}
