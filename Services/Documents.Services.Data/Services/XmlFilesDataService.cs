namespace ProcessingTools.Documents.Services.Data
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Contracts;
    using Models;

    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Documents.Data.Common.Constants;
    using ProcessingTools.Documents.Data.Models;
    using ProcessingTools.Documents.Data.Repositories.Contracts;
    using ProcessingTools.Extensions;

    public class XmlFilesDataService : IXmlFilesDataService
    {
        private const string DefaultDataFilesDirectoryKey = "DefaultDataFilesDirectory";

        private const int MaximalNumberOfTrialsToGenerateNewFileName = 100;

        private readonly IDocumentsRepositoryProvider<Document> repositoryProvider;

        public XmlFilesDataService(IDocumentsRepositoryProvider<Document> repositoryProvider)
        {
            if (repositoryProvider == null)
            {
                throw new ArgumentNullException(nameof(repositoryProvider));
            }

            this.repositoryProvider = repositoryProvider;
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

        public async Task<IQueryable<XmlFileMetadataServiceModel>> All(object userId, object articleId, int pageNumber, int itemsPerPage)
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
                .Select(d => new XmlFileMetadataServiceModel
                {
                    Id = d.Id.ToString(),
                    FileName = d.FileName,
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

        public async Task<object> Create(object userId, object articleId, XmlFileDetailsServiceModel file)
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

            string path = await this.GetNewFilePath(file.FileName);
            file.FileName = Path.GetFileNameWithoutExtension(path);

            var document = new Document
            {
                CreatedByUserId = userId.ToString(),
                ModifiedByUserId = userId.ToString(),
                ContentLength = file.ContentLength,
                ContentType = file.ContentType,
                FileExtension = file.FileExtension,
                FileName = file.FileName
            };

            var repository = this.repositoryProvider.Create();

            File.WriteAllText(path, file.Content);
            await repository.Add(document);
            await repository.SaveChanges();

            repository.TryDispose();

            return new FileInfo(path).Length;
        }

        public async Task<object> Create(object userId, object articleId, XmlFileMetadataServiceModel fileMetadata, Stream inputStream)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (articleId == null)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            if (fileMetadata == null)
            {
                throw new ArgumentNullException(nameof(fileMetadata));
            }

            if (inputStream == null)
            {
                throw new ArgumentNullException(nameof(inputStream));
            }

            string path = await this.GetNewFilePath(fileMetadata.FileName);
            fileMetadata.FileName = Path.GetFileNameWithoutExtension(path);

            var document = new Document
            {
                CreatedByUserId = userId.ToString(),
                ModifiedByUserId = userId.ToString(),
                ContentLength = fileMetadata.ContentLength,
                ContentType = fileMetadata.ContentType,
                FileExtension = fileMetadata.FileExtension,
                FileName = fileMetadata.FileName
            };

            var repository = this.repositoryProvider.Create();

            await this.SaveStreamToFile(path, inputStream);
            await repository.Add(document);
            await repository.SaveChanges();

            repository.TryDispose();

            return new FileInfo(path).Length;
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

            string path = Path.Combine(this.DataDirectory, document.FileName);
            File.Delete(path);
            await repository.Delete(entity: document);
            await repository.SaveChanges();

            repository.TryDispose();

            return fileId;
        }

        public async Task<XmlFileDetailsServiceModel> Get(object userId, object articleId, object fileId)
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

            string path = Path.Combine(this.DataDirectory, document.FileName);
            string content = File.ReadAllText(path);
            var result = new XmlFileDetailsServiceModel
            {
                Id = document.Id.ToString(),
                FileName = document.FileName,
                FileExtension = document.FileExtension,
                ContentLength = document.ContentLength,
                ContentType = document.ContentType,
                Content = content,
                DateCreated = document.DateCreated,
                DateModified = document.DateModified
            };

            repository.TryDispose();

            return result;
        }

        public async Task<object> Update(object userId, object articleId, XmlFileDetailsServiceModel file)
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

            string path = Path.Combine(this.DataDirectory, file.FileName);
            File.WriteAllText(path, file.Content);
            var contentLength = new FileInfo(path).Length;

            document.ModifiedByUserId = userId.ToString();
            document.DateModified = DateTime.UtcNow;
            document.ContentLength = contentLength;
            document.ContentType = file.ContentType;

            await repository.Update(entity: document);
            await repository.SaveChanges();

            repository.TryDispose();

            return contentLength;
        }

        private string GenerateFileName(object id)
        {
            Regex matchInvalidFileNameSymbols = new Regex(@"[^A-Za-z0-9_\-\.]");
            string prefix = matchInvalidFileNameSymbols.Replace(id.ToString(), "_");

            prefix = Path.GetFileNameWithoutExtension(prefix);
            prefix = prefix.Substring(0, Math.Min(prefix.Length, ValidationConstants.LengthOfDocumentFileName / 2));

            string timeStamp = matchInvalidFileNameSymbols.Replace(DateTime.UtcNow.ToString("yyyy-MM-dd-HH-mm-ss"), "-");

            string fileName = $"{prefix}-{timeStamp}-{Guid.NewGuid().ToString()}";

            return fileName.PadRight(ValidationConstants.LengthOfDocumentFileName, 'X')
                .Substring(0, ValidationConstants.LengthOfDocumentFileName);
        }

        private Task<string> GetNewFilePath(string oldFileName)
        {
            return Task.Run(() =>
            {
                string path = Path.Combine(this.DataDirectory, this.GenerateFileName(oldFileName));
                string filePath = path;
                int pathLength = path.Length;

                for (int i = 0; (i < MaximalNumberOfTrialsToGenerateNewFileName) && File.Exists(filePath); ++i)
                {
                    string suffix = i.ToString();
                    filePath = path.Substring(0, pathLength - suffix.Length) + suffix;
                }

                if (File.Exists(filePath))
                {
                    throw new ApplicationException("Can not generate unique file name.");
                }

                return filePath;
            });
        }

        private async Task<long> SaveStreamToFile(string path, Stream stream)
        {
            long streamLength = stream.Length;
            if (streamLength > 0)
            {
                using (var fileStream = File.Create(path, (int)streamLength))
                {
                    byte[] buffer = new byte[stream.Length];
                    await stream.ReadAsync(buffer, 0, buffer.Length);

                    await fileStream.WriteAsync(buffer, 0, buffer.Length);
                }
            }

            return streamLength;
        }
    }
}
