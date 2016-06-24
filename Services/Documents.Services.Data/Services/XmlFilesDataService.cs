namespace ProcessingTools.Documents.Services.Data
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Models;

    using ProcessingTools.Common;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Documents.Data.Common.Constants;
    using ProcessingTools.Documents.Data.Models;
    using ProcessingTools.Documents.Data.Repositories.Contracts;
    using ProcessingTools.Extensions;
    using ProcessingTools.FileSystem.Contracts;

    public class XmlFilesDataService : IXmlFilesDataService
    {
        private const string DefaultDataFilesDirectoryKey = "DefaultDataFilesDirectory";

        private readonly IDocumentsRepositoryProvider<Document> repositoryProvider;
        private readonly IXmlFileReaderWriter xmlFileReaderWriter;

        public XmlFilesDataService(IDocumentsRepositoryProvider<Document> repositoryProvider, IXmlFileReaderWriter xmlFileReaderWriter)
        {
            if (repositoryProvider == null)
            {
                throw new ArgumentNullException(nameof(repositoryProvider));
            }

            if (xmlFileReaderWriter == null)
            {
                throw new ArgumentNullException(nameof(xmlFileReaderWriter));
            }

            this.repositoryProvider = repositoryProvider;
            this.xmlFileReaderWriter = xmlFileReaderWriter;
        }

        private string DataDirectory
        {
            get
            {
                string path = ConfigurationManager.AppSettings[DefaultDataFilesDirectoryKey];
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return path;
            }
        }

        public async Task<IQueryable<DocumentServiceModel>> All(object userId, object articleId, int pageNumber, int itemsPerPage)
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

            if (1 > itemsPerPage || itemsPerPage > DefaultPagingConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidItemsPerPageException();
            }

            var repository = this.repositoryProvider.Create();

            var files = (await repository.All())
                .Where(d => d.CreatedByUserId == userId.ToString())
                //// TODO // .Where(d => d.Article.Id.ToString() == articleId.ToString())
                .OrderByDescending(d => d.DateModified)
                .Skip(pageNumber * itemsPerPage)
                .Take(itemsPerPage)
                .Select(d => new DocumentServiceModel
                {
                    Id = d.Id.ToString(),
                    FileName = d.OriginalFileName,
                    FileExtension = d.FileExtension,
                    ContentType = d.ContentType,
                    ContentLength = d.ContentLength,
                    DateCreated = d.DateCreated,
                    DateModified = d.DateModified
                })
                .ToList();

            repository.TryDispose();

            return files.AsQueryable();
        }

        public async Task<object> Create(object userId, object articleId, DocumentServiceModel file, Stream inputStream)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (articleId == null)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            if (inputStream == null)
            {
                throw new ArgumentNullException(nameof(inputStream));
            }

            string path = await this.xmlFileReaderWriter.GetNewFilePath(file.FileName, this.DataDirectory, ValidationConstants.LengthOfDocumentFileName);

            var document = new Document
            {
                OriginalFileName = file.FileName,
                OriginalContentLength = file.ContentLength,
                ContentType = file.ContentType,
                FileExtension = file.FileExtension,
                CreatedByUserId = userId.ToString(),
                ModifiedByUserId = userId.ToString()
            };

            var repository = this.repositoryProvider.Create();

            document.FileName = Path.GetFileNameWithoutExtension(path);
            document.ContentLength = await this.xmlFileReaderWriter.Write(inputStream, document.FileName, this.DataDirectory);

            await repository.Add(document);
            await repository.SaveChanges();

            repository.TryDispose();

            return document.ContentLength;
        }

        public async Task<object> Delete(object userId, object articleId, object fileId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (articleId == null)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            if (fileId == null)
            {
                throw new ArgumentNullException(nameof(fileId));
            }

            var repository = this.repositoryProvider.Create();

            var document = (await repository.All())
                .Where(d => d.CreatedByUserId == userId.ToString())
                //// TODO // .Where(d => d.Article.Id.ToString() == articleId.ToString())
                .FirstOrDefault(d => d.Id.ToString() == fileId.ToString());

            await this.xmlFileReaderWriter.Delete(document.FileName, this.DataDirectory);

            await repository.Delete(entity: document);
            await repository.SaveChanges();

            repository.TryDispose();

            return fileId;
        }

        public async Task<DocumentServiceModel> Get(object userId, object articleId, object fileId)
        {
            var document = await this.GetDocument(userId, articleId, fileId);
            return new DocumentServiceModel
            {
                Id = document.Id.ToString(),
                ContentLength = document.ContentLength,
                ContentType = document.ContentType,
                DateCreated = document.DateCreated,
                DateModified = document.DateModified,
                FileExtension = document.FileExtension.Trim('.'),
                FileName = document.OriginalFileName
            };
        }

        public async Task<XmlReader> GetReader(object userId, object articleId, object fileId)
        {
            var document = await this.GetDocument(userId, articleId, fileId);
            return this.xmlFileReaderWriter.GetXmlReader(document.FileName, this.DataDirectory);
        }

        public async Task<Stream> GetStream(object userId, object articleId, object fileId)
        {
            var document = await this.GetDocument(userId, articleId, fileId);
            return this.xmlFileReaderWriter.ReadToStream(document.FileName, this.DataDirectory);
        }

        public async Task<object> Update(object userId, object articleId, DocumentServiceModel file, string content)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (articleId == null)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            var repository = this.repositoryProvider.Create();

            var document = (await repository.All())
                .Where(d => d.CreatedByUserId == userId.ToString())
                //// TODO // .Where(d => d.Article.Id.ToString() == articleId.ToString())
                .FirstOrDefault(d => d.Id.ToString() == file.Id);

            using (var stream = new MemoryStream(Defaults.DefaultEncoding.GetBytes(content)))
            {
                document.ContentLength = await this.xmlFileReaderWriter.Write(stream, document.FileName, this.DataDirectory);
                document.ModifiedByUserId = userId.ToString();
                document.DateModified = DateTime.UtcNow;
                document.ContentType = file.ContentType;
            }

            await repository.Update(entity: document);
            await repository.SaveChanges();

            repository.TryDispose();

            return document.ContentLength;
        }

        private async Task<Document> GetDocument(object userId, object articleId, object fileId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (articleId == null)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            if (fileId == null)
            {
                throw new ArgumentNullException(nameof(fileId));
            }

            var repository = this.repositoryProvider.Create();

            var document = (await repository.All())
                .Where(d => d.CreatedByUserId == userId.ToString())
                //// TODO // .Where(d => d.Article.Id.ToString() == articleId.ToString())
                .FirstOrDefault(d => d.Id.ToString() == fileId.ToString());

            repository.TryDispose();

            return document;
        }
    }
}
