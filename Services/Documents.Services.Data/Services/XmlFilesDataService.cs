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

    public class XmlFilesDataService : IXmlFilesDataService
    {
        private const string DefaultDataFilesDirectoryKey = "DefaultDataFilesDirectory";

        private const int MaximalNumberOfTrialsToGenerateNewFileName = 100;

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

        public Task<IQueryable<XmlFileMetadataServiceModel>> All(object userId, object articleId, int pageNumber, int itemsPerPage)
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

            return Task.Run(() =>
            {
                var files = Directory.GetFiles(this.DataDirectory)
                    .Select(file =>
                    {
                        var fileInfo = new FileInfo(file);
                        return new XmlFileMetadataServiceModel
                        {
                            FileName = Path.GetFileName(file),
                            DateCreated = fileInfo.CreationTimeUtc,
                            DateModified = fileInfo.LastWriteTime
                        };
                    })
                    .OrderByDescending(i => i.DateModified)
                    .Skip(pageNumber * itemsPerPage)
                    .Take(itemsPerPage)
                    .ToList();

                return files.AsQueryable();
            });
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

            File.WriteAllText(path, file.Content);

            file.FileName = Path.GetFileNameWithoutExtension(path);
            file.FileExtension = Path.GetExtension(path);

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

            await this.SaveStreamToFile(path, inputStream);

            fileMetadata.FileName = Path.GetFileNameWithoutExtension(path);
            fileMetadata.FileExtension = Path.GetExtension(path);

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

            string path = await this.GetFilePathById(fileId);

            File.Delete(path);

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

            string path = await this.GetFilePathById(fileId);

            string content = File.ReadAllText(path);

            var fileInfo = new FileInfo(path);

            return new XmlFileDetailsServiceModel
            {
                FileName = Path.GetFileName(path),
                FileExtension = Path.GetExtension(path),
                DateCreated = fileInfo.CreationTimeUtc,
                DateModified = fileInfo.LastWriteTime,
                Content = content,
                ContentLength = fileInfo.Length
            };
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

            string path = await this.GetFilePathById(file.Id);

            File.WriteAllText(path, file.Content);

            file.FileName = Path.GetFileNameWithoutExtension(path);
            file.FileExtension = Path.GetExtension(path);

            return new FileInfo(path).Length;
        }

        private Task<string> GetFilePathById(object id)
        {
            return Task.Run(() =>
            {
                string fileName = Directory.GetFiles(this.DataDirectory)
                    .FirstOrDefault(f => Path.GetFileName(f).GetHashCode().ToString() == id.ToString());

                return fileName;
            });
        }

        private string GenerateFileName(object id)
        {
            Regex matchInvalidFileNameSymbols = new Regex(@"[^A-Za-z0-9_\-\.]");
            string prefix = matchInvalidFileNameSymbols.Replace(id.ToString(), "_");
            return Path.GetFileNameWithoutExtension(prefix) + "-" +
                matchInvalidFileNameSymbols.Replace(DateTime.UtcNow.ToString(), "-") +
                Guid.NewGuid().ToString().GetHashCode();
        }

        private Task<string> GetNewFilePath(string oldFileName)
        {
            return Task.Run(() =>
            {
                string path = this.DataDirectory + "/" + this.GenerateFileName(oldFileName);
                string filePath = path;

                for (int i = 0; (i < MaximalNumberOfTrialsToGenerateNewFileName) && File.Exists(filePath); ++i)
                {
                    filePath = path + i;
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
