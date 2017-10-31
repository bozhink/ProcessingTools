namespace ProcessingTools.Documents.Services.Data.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Common.Extensions.Linq;
    using ProcessingTools.Constants;
    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Contracts.IO;
    using ProcessingTools.Data.Contracts.Repositories;
    using ProcessingTools.Documents.Data.Entity.Contracts.Repositories;
    using ProcessingTools.Documents.Services.Data.Contracts;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Services.Models.Contracts.Data.Documents;
    using ProcessingTools.Services.Models.Data.Documents;

    public class DocumentsDataService : IDocumentsDataService
    {
        private readonly IDocumentsRepositoryProvider<Documents.Data.Entity.Models.Document> repositoryProvider;
        private readonly IXmlFileReaderWriter xmlFileReaderWriter;

        public DocumentsDataService(IDocumentsRepositoryProvider<Documents.Data.Entity.Models.Document> repositoryProvider, IXmlFileReaderWriter xmlFileReaderWriter)
        {
            this.repositoryProvider = repositoryProvider ?? throw new ArgumentNullException(nameof(repositoryProvider));
            this.xmlFileReaderWriter = xmlFileReaderWriter ?? throw new ArgumentNullException(nameof(xmlFileReaderWriter));
        }

        // TODO: ConfigurationManager
        private string DataDirectory
        {
            get
            {
                string path = AppSettings.AppDataDirectoryName;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return path;
            }
        }

        public async Task<IEnumerable<IDocument>> All(object userId, object articleId, int pageNumber, int itemsPerPage)
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

            if (itemsPerPage < 1 || itemsPerPage > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException();
            }

            var repository = this.repositoryProvider.Create();

            var query = repository.Query
                .Where(d => d.CreatedBy == userId.ToString())
                //// TODO // .Where(d => d.Article.Id.ToString() == articleId.ToString())
                .OrderByDescending(d => d.ModifiedOn)
                .Skip(pageNumber * itemsPerPage)
                .Take(itemsPerPage)
                .Select(d => new ProcessingTools.Services.Models.Data.Documents.Document
                {
                    Id = d.Id.ToString(),
                    FileName = d.FileName,
                    FileExtension = d.FileExtension,
                    Comment = d.Comment,
                    ContentType = d.ContentType,
                    ContentLength = d.ContentLength,
                    DateCreated = d.CreatedOn,
                    DateModified = d.ModifiedOn
                });

            var documents = await query.ToListAsync().ConfigureAwait(false);

            repository.TryDispose();

            return documents;
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
                .Where(d => d.CreatedBy == userId.ToString())
                //// TODO // .Where(d => d.Article.Id.ToString() == articleId.ToString())
                .LongCountAsync()
                .ConfigureAwait(false);

            repository.TryDispose();

            return count;
        }

        public async Task<object> Create(object userId, object articleId, IDocument document, Stream inputStream)
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

            string path = await this.xmlFileReaderWriter.GetNewFilePathAsync(document.FileName, this.DataDirectory, ProcessingTools.Constants.Data.Documents.ValidationConstants.MaximalLengthOfFullFileName).ConfigureAwait(false);

            var entity = new Documents.Data.Entity.Models.Document
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

                CreatedBy = userId.ToString(),
                ModifiedBy = userId.ToString()
            };

            var repository = this.repositoryProvider.Create();

            entity.FilePath = Path.GetFileNameWithoutExtension(path);
            entity.ContentLength = await this.xmlFileReaderWriter.WriteAsync(inputStream, entity.FilePath, this.DataDirectory).ConfigureAwait(false);

            await repository.AddAsync(entity).ConfigureAwait(false);
            await repository.SaveChangesAsync().ConfigureAwait(false);

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

            var entity = await this.GetEntityAsync(userId, articleId, documentId, repository).ConfigureAwait(false);

            await this.xmlFileReaderWriter.DeleteAsync(entity.FilePath, this.DataDirectory).ConfigureAwait(false);

            await repository.DeleteAsync(entity.Id).ConfigureAwait(false);
            await repository.SaveChangesAsync().ConfigureAwait(false);

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
                .Where(d => d.CreatedBy == userId.ToString())
                //// TODO // .Where(d => d.Article.Id.ToString() == articleId.ToString())
                .AsEnumerable();

            foreach (var entity in entities)
            {
                await this.xmlFileReaderWriter.DeleteAsync(entity.FilePath, this.DataDirectory).ConfigureAwait(false);
                await repository.DeleteAsync(entity.Id).ConfigureAwait(false);
            }

            var result = await repository.SaveChangesAsync().ConfigureAwait(false);

            repository.TryDispose();

            return result;
        }

        public async Task<IDocument> Get(object userId, object articleId, object documentId)
        {
            var entity = await this.GetDocument(userId, articleId, documentId).ConfigureAwait(false);
            return new ProcessingTools.Services.Models.Data.Documents.Document
            {
                Id = entity.Id.ToString(),
                FileName = entity.FileName,
                FileExtension = entity.FileExtension.Trim('.'),
                ContentLength = entity.ContentLength,
                ContentType = entity.ContentType,
                Comment = entity.Comment,
                DateCreated = entity.CreatedOn,
                DateModified = entity.ModifiedOn
            };
        }

        public async Task<XmlReader> GetReader(object userId, object articleId, object documentId)
        {
            var entity = await this.GetDocument(userId, articleId, documentId).ConfigureAwait(false);
            return this.xmlFileReaderWriter.GetXmlReader(entity.FilePath, this.DataDirectory);
        }

        public async Task<Stream> GetStream(object userId, object articleId, object documentId)
        {
            var entity = await this.GetDocument(userId, articleId, documentId).ConfigureAwait(false);
            return this.xmlFileReaderWriter.ReadToStream(entity.FilePath, this.DataDirectory);
        }

        public async Task<object> UpdateContent(object userId, object articleId, IDocument document, string content)
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

            var entity = await this.GetEntityAsync(userId, articleId, document.Id, repository).ConfigureAwait(false);

            using (var stream = new MemoryStream(Defaults.Encoding.GetBytes(content)))
            {
                entity.ContentLength = await this.xmlFileReaderWriter.WriteAsync(stream, entity.FilePath, this.DataDirectory).ConfigureAwait(false);
                entity.ModifiedBy = userId.ToString();
                entity.ModifiedOn = DateTime.UtcNow;
                entity.ContentType = document.ContentType;
                stream.Close();
            }

            await repository.UpdateAsync(entity: entity).ConfigureAwait(false);
            await repository.SaveChangesAsync().ConfigureAwait(false);

            repository.TryDispose();

            return entity.ContentLength;
        }

        public async Task<object> UpdateMeta(object userId, object articleId, IDocument document)
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

            var entity = await this.GetEntityAsync(userId, articleId, document.Id, repository).ConfigureAwait(false);

            entity.Comment = document.Comment;
            entity.ContentType = document.ContentType;
            entity.FileExtension = document.FileExtension;
            entity.FileName = document.FileName;
            entity.ModifiedBy = userId.ToString();
            entity.ModifiedOn = DateTime.UtcNow;

            await repository.UpdateAsync(entity: entity).ConfigureAwait(false);
            await repository.SaveChangesAsync().ConfigureAwait(false);

            repository.TryDispose();

            return entity.ContentLength;
        }

        private async Task<Documents.Data.Entity.Models.Document> GetDocument(object userId, object articleId, object documentId)
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

            var entity = await this.GetEntityAsync(userId, articleId, documentId, repository).ConfigureAwait(false);

            repository.TryDispose();

            return entity;
        }

        private async Task<Documents.Data.Entity.Models.Document> GetEntityAsync(object userId, object articleId, object documentId, ICrudRepository<Documents.Data.Entity.Models.Document> repository)
        {
            var entity = await repository.Query
                .Where(d => d.CreatedBy == userId.ToString())
                //// TODO: // .Where(d => d.Article.Id.ToString() == articleId.ToString())
                .FirstOrDefaultAsync(d => d.Id.ToString() == documentId.ToString())
                .ConfigureAwait(false);

            if (entity == null)
            {
                repository.TryDispose();
                throw new EntityNotFoundException();
            }

            return entity;
        }
    }
}
