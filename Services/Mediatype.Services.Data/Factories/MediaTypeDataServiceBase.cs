namespace ProcessingTools.MediaType.Services.Data.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Models;
    using Models.Contracts;

    public abstract class MediaTypeDataServiceBase : IMediaTypeDataService
    {
        protected const string DefaultMediaType = "unknown/unknown";
        protected const string DefaultMediaTypeOnException = "application/octet-stream";

        public abstract IQueryable<IMediaTypeServiceModel> GetMediaType(string fileExtension);

        protected string GetValidFileExtension(string fileExtension)
        {
            string extension = fileExtension?.TrimStart('.', ' ', '\n', '\r');
            if (string.IsNullOrWhiteSpace(extension))
            {
                throw new ArgumentNullException(nameof(fileExtension));
            }

            return extension;
        }

        protected IQueryable<IMediaTypeServiceModel> GetSingleStringMediaTypeResultAsQueryable(string extension, string mediaType)
        {
            var result = new Queue<IMediaTypeServiceModel>();

            int slashIndex = mediaType.IndexOf('/');
            result.Enqueue(new MediaTypeServiceModel
            {
                FileExtension = extension,
                MimeType = mediaType.Substring(0, slashIndex),
                MimeSubtype = mediaType.Substring(slashIndex + 1)
            });

            return result.AsQueryable();
        }
    }
}