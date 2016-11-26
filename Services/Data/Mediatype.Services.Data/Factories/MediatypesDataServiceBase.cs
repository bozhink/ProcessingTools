namespace ProcessingTools.Mediatypes.Services.Data.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Models;

    public abstract class MediatypesDataServiceBase : IMediatypesDataService
    {
        protected const string DefaultMediaType = "unknown/unknown";
        protected const string DefaultMediaTypeOnException = "application/octet-stream";

        public abstract Task<IQueryable<MediatypeServiceModel>> GetMediaType(string fileExtension);

        protected string GetValidFileExtension(string fileExtension)
        {
            string extension = fileExtension?.TrimStart('.', ' ', '\n', '\r');
            if (string.IsNullOrWhiteSpace(extension))
            {
                throw new ArgumentNullException(nameof(fileExtension));
            }

            return extension;
        }

        protected IQueryable<MediatypeServiceModel> GetSingleStringMediaTypeResultAsQueryable(string extension, string mediaType)
        {
            var result = new Queue<MediatypeServiceModel>();

            int slashIndex = mediaType.IndexOf('/');
            result.Enqueue(new MediatypeServiceModel
            {
                FileExtension = extension,
                Mimetype = mediaType.Substring(0, slashIndex),
                Mimesubtype = mediaType.Substring(slashIndex + 1)
            });

            return result.AsQueryable();
        }
    }
}