// <copyright file="FilesDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Files
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts.Models.Files;
    using ProcessingTools.Contracts.Services.Files;
    using ProcessingTools.Services.Models.Data.Files;

    /// <summary>
    /// Files data service.
    /// </summary>
    public class FilesDataService : IFilesDataService
    {
        private readonly IMimeMappingService mimeMappingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilesDataService"/> class.
        /// </summary>
        /// <param name="mimeMappingService">MIME mapping service.</param>
        public FilesDataService(IMimeMappingService mimeMappingService)
        {
            this.mimeMappingService = mimeMappingService ?? throw new ArgumentNullException(nameof(mimeMappingService));
        }

        /// <inheritdoc/>
        public Task<IFileMetadata> CreateAsync(IFileMetadata metadata, Stream stream) => this.UpdateAsync(metadata, stream);

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public Task<IFileMetadata> GetMetadataAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.GetMetadataInternalAsync(id);
        }

        /// <inheritdoc/>
        public StreamReader GetReader(object id)
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

            var reader = new StreamReader(fileName);
            return reader;
        }

        /// <inheritdoc/>
        public Stream ReadToStream(object id)
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

            var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            return stream;
        }

        /// <inheritdoc/>
        public Task<IFileMetadata> UpdateAsync(IFileMetadata metadata)
        {
            if (metadata == null)
            {
                throw new ArgumentNullException(nameof(metadata));
            }

            return Task.FromResult(metadata);
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public Task<IFileMetadata> UpdateAsync(object id, Stream stream)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return this.UpdateInternalAsync(id, stream);
        }

        /// <inheritdoc/>
        public Task<object> WriteAsync(object id, StreamReader streamReader)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (streamReader == null)
            {
                throw new ArgumentNullException(nameof(streamReader));
            }

            return this.WriteAsync(id, streamReader.BaseStream);
        }

        /// <inheritdoc/>
        public Task<object> WriteAsync(object id, Stream stream)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanRead)
            {
                throw new StreamCannotBeReadException();
            }

            return this.WriteInternalAsync(id, stream);
        }

        private async Task<IFileMetadata> GetMetadataInternalAsync(object id)
        {
            string fileName = id.ToString();
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new FileNameIsNullOrWhitespaceException();
            }

            return await this.GetSystemFileMetadataAsync(fileName).ConfigureAwait(false);
        }

        private async Task<IFileMetadata> GetSystemFileMetadataAsync(string fullName)
        {
            var fileInfo = new FileInfo(fullName);

            string contentType = await this.mimeMappingService.MapAsync(fileInfo.FullName).ConfigureAwait(false);

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
                CreatedBy = "NA",
                ModifiedBy = "NA",
            };
        }

        private async Task<IFileMetadata> UpdateInternalAsync(object id, Stream stream)
        {
            var fullName = id.ToString();

            await this.WriteAsync(fullName, stream).ConfigureAwait(false);

            return await this.GetSystemFileMetadataAsync(fullName).ConfigureAwait(false);
        }

        private async Task<object> WriteInternalAsync(object id, Stream stream)
        {
            string fileName = id.ToString();
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new FileNameIsNullOrWhitespaceException();
            }

            long writtenStreamLength = 0L;
            using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                await stream.CopyToAsync(fileStream).ConfigureAwait(false);
                await fileStream.FlushAsync().ConfigureAwait(false);
                writtenStreamLength = fileStream.Length;
                fileStream.Close();
            }

            return writtenStreamLength;
        }
    }
}
