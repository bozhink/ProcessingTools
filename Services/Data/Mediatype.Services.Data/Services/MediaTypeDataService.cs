namespace ProcessingTools.Mediatypes.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Factories;
    using Models;
    using ProcessingTools.Mediatypes.Data.Entity.Contracts.Repositories;
    using ProcessingTools.Mediatypes.Data.Entity.Models;

    public class MediaTypeDataService : MediaTypeDataServiceBase, IMediaTypeDataService
    {
        private IMediatypesRepository<FileExtension> repository;

        public MediaTypeDataService(IMediatypesRepository<FileExtension> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            this.repository = repository;
        }

        public override async Task<IQueryable<MediaTypeServiceModel>> GetMediaType(string fileExtension)
        {
            string extension = this.GetValidFileExtension(fileExtension);

            FileExtension fileExtensionResult;
            try
            {
                fileExtensionResult = (await this.repository.All())
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
                .Select<MimetypePair, MediaTypeServiceModel>(p => new MediaTypeServiceModel
                {
                    FileExtension = fileExtensionResult.Name,
                    MimeType = p.MimeType.Name,
                    MimeSubtype = p.MimeSubtype.Name
                });
        }
    }
}
