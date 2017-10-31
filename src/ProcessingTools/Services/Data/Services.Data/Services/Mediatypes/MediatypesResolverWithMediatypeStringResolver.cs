namespace ProcessingTools.Services.Data.Services.Mediatypes
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Models.Contracts.Mediatypes;
    using ProcessingTools.Services.Data.Contracts.Mediatypes;
    using ProcessingTools.Services.Models.Data.Mediatypes;

    public class MediatypesResolverWithMediatypeStringResolver : IMediatypesResolver
    {
        private readonly IMediatypeStringResolver mediatypeStringResolver;

        public MediatypesResolverWithMediatypeStringResolver(IMediatypeStringResolver mediatypeStringResolver)
        {
            this.mediatypeStringResolver = mediatypeStringResolver ?? throw new ArgumentNullException(nameof(mediatypeStringResolver));
        }

        public async Task<IEnumerable<IMediatype>> ResolveMediatype(string fileExtension)
        {
            string extension = fileExtension?.Trim('.', ' ', '\n', '\r');
            if (string.IsNullOrWhiteSpace(extension))
            {
                throw new ArgumentNullException(nameof(fileExtension));
            }

            string mediatype = await this.mediatypeStringResolver.Resolve($".{extension.ToLowerInvariant()}").ConfigureAwait(false);

            return new IMediatype[]
            {
                new Mediatype(mediatype)
            };
        }
    }
}
