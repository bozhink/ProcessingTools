namespace ProcessingTools.Services.Data.Services.Mediatypes
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Contracts.Mediatypes;
    using Models.Mediatypes;
    using ProcessingTools.Contracts.Models.Mediatypes;

    public class MediatypesResolverWithMediatypeStringResolver : IMediatypesResolver
    {
        private readonly IMediatypeStringResolver mediatypeStringResolver;

        public MediatypesResolverWithMediatypeStringResolver(IMediatypeStringResolver mediatypeStringResolver)
        {
            if (mediatypeStringResolver == null)
            {
                throw new ArgumentNullException(nameof(mediatypeStringResolver));
            }

            this.mediatypeStringResolver = mediatypeStringResolver;
        }

        public async Task<IEnumerable<IMediatype>> ResolveMediatype(string fileExtension)
        {
            string extension = fileExtension?.Trim('.', ' ', '\n', '\r');
            if (string.IsNullOrWhiteSpace(extension))
            {
                throw new ArgumentNullException(nameof(fileExtension));
            }

            string mediatype = await this.mediatypeStringResolver.Resolve($".{extension.ToLower()}");

            return new IMediatype[]
            {
                new MediatypeServiceModel(mediatype)
            };
        }
    }
}
