namespace ProcessingTools.MediaType.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Models;
    using Models.Contracts;
    using ProcessingTools.MediaType.Services.Data.Contracts;

    public abstract class MediaTypeDataServiceBase : IMediaTypeDataService
    {
        protected const string DefaultMediaType = "unknown/unknown";
        protected const string DefaultMediaTypeOnException = "application/octet-stream";

        public abstract IQueryable<IMediaType> GetMediaType(string fileExtension);

        protected string GetValidFileExtension(string fileExtension)
        {
            string extension = fileExtension?.TrimStart('.', ' ', '\n', '\r');
            if (string.IsNullOrWhiteSpace(extension))
            {
                throw new ArgumentNullException("fileExtension");
            }

            return extension;
        }

        protected IQueryable<IMediaType> GetSingleStringMediaTypeResultAsQueryable(string extension, string mediaType)
        {
            var result = new Queue<IMediaType>();

            int slashIndex = mediaType.IndexOf('/');
            result.Enqueue(new MediaTypeDataServiceResponseModel
            {
                FileExtension = extension,
                MimeType = mediaType.Substring(0, slashIndex),
                MimeSubtype = mediaType.Substring(slashIndex + 1)
            });

            return result.AsQueryable();
        }
    }
}