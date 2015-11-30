namespace ProcessingTools.MediaType.Services.Data
{
    using System;
    using System.Linq;
    using Contracts;
    using MediaType.Data.Models;
    using MediaType.Data.Repositories;
    using Models;

    public class MediaTypeDataService : IMediaTypeDataService
    {
        private IMediaTypesRepository<FileExtension> fileExtensions;

        public MediaTypeDataService(IMediaTypesRepository<FileExtension> fileExtensions)
        {
            this.fileExtensions = fileExtensions;
        }

        public IQueryable<MediaTypeDataServiceResponseModel> GetMediaType(string fileExtension)
        {
            if (string.IsNullOrWhiteSpace(fileExtension))
            {
                throw new ArgumentNullException("fileExtension");
            }

            var extension = this.fileExtensions.All()
                .FirstOrDefault(e => e.Name == fileExtension);

            if (extension == null)
            {
                return null;
            }

            var pairs = extension.MimeTypePairs.AsQueryable();

            if (pairs == null)
            {
                return null;
            }

            return pairs
                .Select(p => new MediaTypeDataServiceResponseModel
                {
                    FileExtension = extension.Name,
                    MimeType = p.MimeType.Name,
                    MimeSubtype = p.MimeSubtype.Name
                });
        }
    }
}