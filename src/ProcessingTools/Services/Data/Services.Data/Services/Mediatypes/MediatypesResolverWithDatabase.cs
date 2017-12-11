namespace ProcessingTools.Services.Data.Services.Mediatypes
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Extensions.Linq;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Data.Repositories.Mediatypes;
    using ProcessingTools.Contracts.Models.Mediatypes;
    using ProcessingTools.Contracts.Services.Data.Mediatypes;
    using ProcessingTools.Services.Models.Data.Mediatypes;

    public class MediatypesResolverWithDatabase : IMediatypesResolver
    {
        private readonly ISearchableMediatypesRepository repository;

        public MediatypesResolverWithDatabase(ISearchableMediatypesRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IMediatype[]> ResolveMediatypeAsync(string fileExtension)
        {
            string extension = fileExtension?.Trim('.', ' ', '\n', '\r');
            if (string.IsNullOrWhiteSpace(extension))
            {
                throw new ArgumentNullException(nameof(fileExtension));
            }

            try
            {
                var response = await this.repository.GetByFileExtension(extension).ToListAsync().ConfigureAwait(false);

                if (response == null || response.Count < 1)
                {
                    return this.GetStaticResult(ContentTypes.DefaultMimetype, ContentTypes.DefaultMimesubtype);
                }
                else
                {
                    return response.Select(e => new Mediatype(e.Mimetype, e.Mimesubtype)).ToArray();
                }
            }
            catch
            {
                return this.GetStaticResult(ContentTypes.DefaultMimetypeOnException, ContentTypes.DefaultMimesubtypeOnException);
            }
        }

        private IMediatype[] GetStaticResult(string mimetype, string mimesubtype)
        {
            return new IMediatype[]
            {
                new Mediatype(mimetype, mimesubtype)
            };
        }
    }
}
