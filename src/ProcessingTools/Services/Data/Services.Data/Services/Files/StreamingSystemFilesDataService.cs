namespace ProcessingTools.Services.Data.Services.Files
{
    using System;
    using System.IO;
    using System.Security.Principal;
    using System.Threading.Tasks;
    using System.Web;
    using ProcessingTools.Contracts.Models.Files;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Services.Data.Contracts.Files;
    using ProcessingTools.Services.Data.Models.Files;

    public class StreamingSystemFilesDataService : IStreamingSystemFilesDataService
    {
        private readonly IStreamingSystemFileContentDataService fileContentDataService;

        public StreamingSystemFilesDataService(IStreamingSystemFileContentDataService fileContentDataService)
        {
            this.fileContentDataService = fileContentDataService ?? throw new ArgumentNullException(nameof(fileContentDataService));
        }

        public Task<IFileMetadata> Create(IFileMetadata metadata, Stream stream) => this.Update(metadata, stream);

        public Task<bool> Delete(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            string fileName = id.ToString();
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new FileNameIsNullOrWhitespaceException();
            }

            return Task.Run(() =>
            {
                File.Delete(fileName);
                return true;
            });
        }

        public Task<IFileMetadata> GetMetadata(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            string fileName = id.ToString();
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new FileNameIsNullOrWhitespaceException();
            }

            return Task.FromResult(this.GetSystemFileMetadata(fileName));
        }

        public StreamReader GetReader(object id) => this.fileContentDataService.GetReader(id);

        public Stream ReadToStream(object id) => this.fileContentDataService.ReadToStream(id);

        public Task<IFileMetadata> Update(IFileMetadata metadata, Stream stream)
        {
            if (metadata == null)
            {
                throw new ArgumentNullException(nameof(metadata));
            }

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return this.Update(metadata.FullName, stream);
        }

        public async Task<IFileMetadata> Update(object id, Stream stream)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            var fullName = id.ToString();

            await this.fileContentDataService.Write(fullName, stream);

            return this.GetSystemFileMetadata(fullName);
        }

        private IFileMetadata GetSystemFileMetadata(string fullName)
        {
            var fileInfo = new FileInfo(fullName);

            string contentType = MimeMapping.GetMimeMapping(fileInfo.FullName);
            string user = File.GetAccessControl(fileInfo.FullName).GetOwner(typeof(NTAccount)).ToString();

            return new FileMetadataServiceModel
            {
                Id = fileInfo.FullName,
                FileExtension = fileInfo.Extension.Trim('.'),
                FileName = Path.GetFileNameWithoutExtension(fileInfo.Name).Trim('.'),
                FullName = fileInfo.FullName,
                ContentLength = fileInfo.Length,
                ContentType = contentType,
                DateCreated = fileInfo.CreationTimeUtc,
                DateModified = fileInfo.LastWriteTimeUtc,
                CreatedByUser = user,
                ModifiedByUser = user
            };
        }
    }
}
