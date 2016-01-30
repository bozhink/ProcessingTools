namespace ProcessingTools.MediaType.Services.Data
{
    using System;
    using System.Linq;

    using Contracts;
    using Factories;
    using Models;
    using Models.Contracts;

    using ProcessingTools.MediaType.Data.Models;
    using ProcessingTools.MediaType.Data.Repositories.Contracts;

    public class MediaTypeDataService : MediaTypeDataServiceBase, IMediaTypeDataService
    {
        private IMediaTypesRepository<FileExtension> fileExtensions;

        public MediaTypeDataService(IMediaTypesRepository<FileExtension> fileExtensions)
        {
            this.fileExtensions = fileExtensions;
        }

        public override IQueryable<IMediaType> GetMediaType(string fileExtension)
        {
            string extension = this.GetValidFileExtension(fileExtension);

            FileExtension fileExtensionResult;
            try
            {
                fileExtensionResult = this.fileExtensions.All()
                    .FirstOrDefault(e => e.Name == extension);
            }
            catch (ArgumentNullException)
            {
                // FirstOrDefault throws because the IRepository returns empty IQueryable.
                fileExtensionResult = null;
            }

            var pairs = fileExtensionResult?.MimeTypePairs?.AsQueryable();

            if (pairs == null)
            {
                return this.GetSingleStringMediaTypeResultAsQueryable(
                    fileExtension,
                    MediaTypeDataServiceBase.DefaultMediaType);
            }

            return pairs
                .Select<MimeTypePair, IMediaType>(p => new MediaTypeDataServiceResponseModel
                {
                    FileExtension = fileExtensionResult.Name,
                    MimeType = p.MimeType.Name,
                    MimeSubtype = p.MimeSubtype.Name
                });
        }
    }
}