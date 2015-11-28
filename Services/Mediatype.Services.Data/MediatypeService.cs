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

        public IQueryable<MediatypeDataServiceResponseModel> GetMediatype(string fileExtension)
        {
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
                .Select(p => new MediatypeDataServiceResponseModel
                {
                    FileExtension = extension.Name,
                    MimeType = p.MimeType.Name,
                    MimeSubtype = p.MimeSubtype.Name
                });
        }
    }
}