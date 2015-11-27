namespace ProcessingTools.Mediatype.Services.Data
{
    using Mediatype.Data.Models;
    using ProcessingTools.Data.Common.Repositories;
    using System.Linq;

    public class MediatypeService : IMediatypeService
    {
        private IRepository<FileExtension> fileExtensions;

        public MediatypeService(IRepository<FileExtension> fileExtensions)
        {
            this.fileExtensions = fileExtensions;
        }

        public string GetMediatype(string fileExtension)
        {
            string result =  this.fileExtensions.All()
                .Where(e => e.Name == fileExtension)
                .Select(e => e.MimeTypePairs.FirstOrDefault().MimeType + "/" + e.MimeTypePairs.FirstOrDefault().MimeSubtype)
                .FirstOrDefault();

            return result;
        }
    }
}