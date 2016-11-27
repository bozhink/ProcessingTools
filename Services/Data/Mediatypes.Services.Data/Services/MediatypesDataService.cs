namespace ProcessingTools.Mediatypes.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Contracts.Models;
    using Models;
    using ProcessingTools.Extensions.Linq;
    using ProcessingTools.Mediatypes.Data.Common.Contracts.Repositories;

    // TODO: dispose repository
    public class MediatypesDataService : IMediatypesDataService
    {
        private const string DefaultMimetype = "unknown";
        private const string DefaultMimesubtype = "unknown";
        private const string DefaultMimetypeOnException = "application";
        private const string DefaultMimesubtypeOnException = "octet-stream";

        private readonly IMediatypesRepository repository;

        public MediatypesDataService(IMediatypesRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            this.repository = repository;
        }

        public async Task<IEnumerable<IMediatypeServiceModel>> ResolveMediatype(string fileExtension)
        {
            string extension = this.GetValidFileExtension(fileExtension);

            try
            {
                var response = await this.repository.GetByFileExtension(extension).ToListAsync();

                if (response == null || response.Count < 1)
                {
                    return this.GetStaticResult(extension, DefaultMimetype, DefaultMimesubtype);
                }
                else
                {
                    return response.Select(e => new MediatypeServiceModel
                    {
                        FileExtension = e.FileExtension,
                        Mimetype = e.Mimetype,
                        Mimesubtype = e.Mimesubtype
                    });
                }
            }
            catch
            {
                return this.GetStaticResult(extension, DefaultMimetypeOnException, DefaultMimesubtypeOnException);
            }
        }

        private string GetValidFileExtension(string fileExtension)
        {
            string extension = fileExtension?.TrimStart('.', ' ', '\n', '\r');
            if (string.IsNullOrWhiteSpace(extension))
            {
                throw new ArgumentNullException(nameof(fileExtension));
            }

            return extension;
        }

        private IEnumerable<IMediatypeServiceModel> GetStaticResult(string extension, string mimetype, string mimesubtype)
        {
            var result = new IMediatypeServiceModel[1];
            result[0] = new MediatypeServiceModel
            {
                FileExtension = extension,
                Mimetype = mimetype,
                Mimesubtype = mimesubtype
            };

            return result;
        }
    }
}
