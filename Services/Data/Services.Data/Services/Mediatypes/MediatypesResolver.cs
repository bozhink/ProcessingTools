namespace ProcessingTools.Services.Data.Services.Mediatypes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Models.Mediatypes;
    using ProcessingTools.Extensions.Linq;
    using ProcessingTools.Mediatypes.Data.Common.Contracts.Repositories;
    using ProcessingTools.Services.Data.Contracts.Mediatypes;
    using ProcessingTools.Services.Data.Models.Mediatypes;

    // TODO: dispose repository
    public class MediatypesResolver : IMediatypesResolver
    {
        private readonly ISearchableMediatypesRepository repository;

        public MediatypesResolver(ISearchableMediatypesRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            this.repository = repository;
        }

        public async Task<IEnumerable<IMediatype>> ResolveMediatype(string fileExtension)
        {
            string extension = fileExtension?.TrimStart('.', ' ', '\n', '\r');
            if (string.IsNullOrWhiteSpace(extension))
            {
                throw new ArgumentNullException(nameof(fileExtension));
            }

            try
            {
                var response = await this.repository.GetByFileExtension(extension).ToListAsync();

                if (response == null || response.Count < 1)
                {
                    return this.GetStaticResult(MediaTypes.DefaultMimetype, MediaTypes.DefaultMimesubtype);
                }
                else
                {
                    return response.Select(e => new MediatypeServiceModel(e.Mimetype, e.Mimesubtype));
                }
            }
            catch
            {
                return this.GetStaticResult(MediaTypes.DefaultMimetypeOnException, MediaTypes.DefaultMimesubtypeOnException);
            }
        }

        private IEnumerable<IMediatype> GetStaticResult(string mimetype, string mimesubtype)
        {
            return new IMediatype[]
            {
                new MediatypeServiceModel(mimetype, mimesubtype)
            };
        }
    }
}
