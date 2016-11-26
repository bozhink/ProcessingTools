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

    public class MediatypesDataService : MediatypesDataServiceBase, IMediatypesDataService
    {
        private IMediatypesRepository<FileExtension> repository;

        public MediatypesDataService(IMediatypesRepository<FileExtension> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            this.repository = repository;
        }

        public override async Task<IQueryable<MediatypeServiceModel>> GetMediaType(string fileExtension)
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
                    MediatypesDataServiceBase.DefaultMediaType);
            }

            return pairs
                .Select<MimetypePair, MediatypeServiceModel>(p => new MediatypeServiceModel
                {
                    FileExtension = fileExtensionResult.Name,
                    Mimetype = p.MimeType.Name,
                    Mimesubtype = p.MimeSubtype.Name
                });
        }
    }
}
