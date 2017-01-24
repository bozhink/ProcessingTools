namespace ProcessingTools.Services.Data.Services.Mediatypes
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web;
    using ProcessingTools.Contracts.Models.Mediatypes;
    using ProcessingTools.Services.Data.Contracts.Mediatypes;
    using ProcessingTools.Services.Data.Models.Mediatypes;

    public class MediatypesResolverWithSystemWeb : IMediatypesResolver
    {
        public async Task<IEnumerable<IMediatype>> ResolveMediatype(string fileExtension)
        {
            string extension = fileExtension?.Trim('.', ' ', '\n', '\r');
            if (string.IsNullOrWhiteSpace(extension))
            {
                throw new ArgumentNullException(nameof(fileExtension));
            }

            string fileName = "/fake/file-name." + extension;
            string mediatype = MimeMapping.GetMimeMapping(fileName);

            return await Task.FromResult(new IMediatype[]
            {
                new MediatypeServiceModel(mediatype)
            });
        }
    }
}
