namespace ProcessingTools.MediaType.Services.Data
{
    using System;
    using System.Linq;
    using Contracts;
    using MediaType.Data.Models;
    using Models;
    using ProcessingTools.Data.Common.Repositories;

    public class MediaTypeDataService : IMediaTypeDataService
    {
        private IRepository<FileExtension> fileExtensions;

        public MediaTypeDataService(IRepository<FileExtension> fileExtensions)
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