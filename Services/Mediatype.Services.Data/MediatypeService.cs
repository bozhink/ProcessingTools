namespace ProcessingTools.Mediatype.Services.Data
{
    using System.Linq;
    using Contracts;
    using Mediatype.Data.Models;
    using Models;
    using ProcessingTools.Data.Common.Repositories;

    public class MediatypeService : IMediatypeService
    {
        private IRepository<FileExtension> fileExtensions;

        public MediatypeService(IRepository<FileExtension> fileExtensions)
        {
            this.fileExtensions = fileExtensions;
        }

        public MediatypeDataServiceResponseModel GetMediatype(string fileExtension)
        {
            var extension = this.fileExtensions.All()
                .FirstOrDefault(e => e.Name == fileExtension);

            if (extension == null)
            {
                return null;
            }

            var pair = extension.MimeTypePairs.FirstOrDefault();

            if (pair == null)
            {
                return null;
            }

            return new MediatypeDataServiceResponseModel
            {
                FileExtension = extension.Name,
                MimeType = pair.MimeType.Name,
                MimeSubtype = pair.MimeSubtype.Name
            };
        }
    }
}