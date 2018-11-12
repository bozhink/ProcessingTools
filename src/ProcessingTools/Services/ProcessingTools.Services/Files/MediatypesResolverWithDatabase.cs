namespace ProcessingTools.Services.Files
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Data.Contracts.Files;
    using ProcessingTools.Models.Contracts.Files.Mediatypes;
    using ProcessingTools.Services.Contracts.Files;
    using ProcessingTools.Services.Models.Data.Mediatypes;

    public class MediatypesResolverWithDatabase : IMediatypesResolver
    {
        private readonly IMediatypesDataAccessObject dataAccessObject;

        public MediatypesResolverWithDatabase(IMediatypesDataAccessObject dataAccessObject)
        {
            this.dataAccessObject = dataAccessObject ?? throw new ArgumentNullException(nameof(dataAccessObject));
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
                var response = await this.dataAccessObject.GetMediatypesByExtensionAsync(extension).ConfigureAwait(false);

                if (response == null || !response.Any())
                {
                    return this.GetStaticResult(ContentTypes.DefaultMimeType, ContentTypes.DefaultMimeSubtype);
                }
                else
                {
                    return response.Select(e => new Mediatype(e.MimeType, e.MimeSubtype)).ToArray();
                }
            }
            catch
            {
                return this.GetStaticResult(ContentTypes.DefaultMimetypeOnException, ContentTypes.DefaultMimesubtypeOnException);
            }
        }

        private IMediatype[] GetStaticResult(string mimetype, string mimesubtype)
        {
            return new[]
            {
                new Mediatype(mimetype, mimesubtype)
            };
        }
    }
}
