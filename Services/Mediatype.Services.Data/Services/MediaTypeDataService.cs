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
        private IMediaTypesRepository<FileExtension> repository;

        public MediaTypeDataService(IMediaTypesRepository<FileExtension> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            this.repository = repository;
        }

        public override IQueryable<IMediaTypeServiceModel> GetMediaType(string fileExtension)
        {
            string extension = this.GetValidFileExtension(fileExtension);

            FileExtension fileExtensionResult;
            try
            {
                fileExtensionResult = this.repository.All()
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
                .Select<MimeTypePair, IMediaTypeServiceModel>(p => new MediaTypeServiceModel
                {
                    FileExtension = fileExtensionResult.Name,
                    MimeType = p.MimeType.Name,
                    MimeSubtype = p.MimeSubtype.Name
                });
        }
    }
}