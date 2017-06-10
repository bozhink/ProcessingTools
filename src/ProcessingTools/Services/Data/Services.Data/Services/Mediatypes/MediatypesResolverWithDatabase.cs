namespace ProcessingTools.Services.Data.Services.Mediatypes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Extensions.Linq;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Data.Mediatypes.Repositories;
    using ProcessingTools.Contracts.Models.Mediatypes;
    using ProcessingTools.Services.Data.Contracts.Mediatypes;
    using ProcessingTools.Services.Data.Models.Mediatypes;

    public class MediatypesResolverWithDatabase : IMediatypesResolver
    {
        private readonly ISearchableMediatypesRepository repository;

        public MediatypesResolverWithDatabase(ISearchableMediatypesRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<IMediatype>> ResolveMediatype(string fileExtension)
        {
            string extension = fileExtension?.Trim('.', ' ', '\n', '\r');
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
