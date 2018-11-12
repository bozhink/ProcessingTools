namespace ProcessingTools.Services.Files
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Models.Contracts.Files.Mediatypes;
    using ProcessingTools.Services.Contracts.Files;
    using ProcessingTools.Services.Models.Data.Mediatypes;

    public class MediatypesResolverWithMediatypeStringResolver : IMediatypesResolver
    {
        private readonly IMediatypeStringResolver mediatypeStringResolver;

        public MediatypesResolverWithMediatypeStringResolver(IMediatypeStringResolver mediatypeStringResolver)
        {
            this.mediatypeStringResolver = mediatypeStringResolver ?? throw new ArgumentNullException(nameof(mediatypeStringResolver));
        }

        public async Task<IMediatype[]> ResolveMediatypeAsync(string fileExtension)
        {
            string extension = fileExtension?.Trim('.', ' ', '\n', '\r');
            if (string.IsNullOrWhiteSpace(extension))
            {
                throw new ArgumentNullException(nameof(fileExtension));
            }

            string mediatype = await this.mediatypeStringResolver.ResolveAsync($".{extension.ToLowerInvariant()}").ConfigureAwait(false);

            return new[]
            {
                new Mediatype(mediatype)
            };
        }
    }
}
