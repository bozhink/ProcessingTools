namespace ProcessingTools.Services.Data.Services.Files
{
    using System;
    using System.IO;
    using System.Security.Principal;
    using System.Threading.Tasks;
    using System.Web;
    using ProcessingTools.Contracts.Services.Data.Files;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Models.Contracts.Files;
    using ProcessingTools.Services.Models.Data.Files;

    public class StreamingSystemFilesDataService : IStreamingSystemFilesDataService
    {
        private readonly IStreamingSystemFileContentDataService fileContentDataService;

        public StreamingSystemFilesDataService(IStreamingSystemFileContentDataService fileContentDataService)
        {
            this.fileContentDataService = fileContentDataService ?? throw new ArgumentNullException(nameof(fileContentDataService));
        }

        public Task<IFileMetadata> CreateAsync(IFileMetadata metadata, Stream stream) => this.UpdateAsync(metadata, stream);

        public Task<bool> DeleteAsync(object id)
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

        public Task<IFileMetadata> GetMetadataAsync(object id)
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

        public Task<IFileMetadata> UpdateAsync(IFileMetadata metadata, Stream stream)
        {
            if (metadata == null)
            {
                throw new ArgumentNullException(nameof(metadata));
            }

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return this.UpdateAsync(metadata.FullName, stream);
        }

        public async Task<IFileMetadata> UpdateAsync(object id, Stream stream)
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

            await this.fileContentDataService.WriteAsync(fullName, stream).ConfigureAwait(false);

            return this.GetSystemFileMetadata(fullName);
        }

        private IFileMetadata GetSystemFileMetadata(string fullName)
        {
            var fileInfo = new FileInfo(fullName);

            string contentType = MimeMapping.GetMimeMapping(fileInfo.FullName);
            string user = File.GetAccessControl(fileInfo.FullName).GetOwner(typeof(NTAccount)).ToString();

            return new FileMetadata
            {
                Id = fileInfo.FullName,
                FileExtension = fileInfo.Extension.Trim('.'),
                FileName = Path.GetFileNameWithoutExtension(fileInfo.Name).Trim('.'),
                FullName = fileInfo.FullName,
                ContentLength = fileInfo.Length,
                ContentType = contentType,
                CreatedOn = fileInfo.CreationTimeUtc,
                ModifiedOn = fileInfo.LastWriteTimeUtc,
                CreatedBy = user,
                ModifiedBy = user
            };
        }
    }
}
