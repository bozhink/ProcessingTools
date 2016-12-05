namespace ProcessingTools.Mediatypes.Data.Internal.Abstractions.Repositories
{
    using System;
    using System.Collections.Generic;
    using ProcessingTools.Mediatypes.Data.Common.Contracts.Models;
    using ProcessingTools.Mediatypes.Data.Common.Contracts.Repositories;
    using ProcessingTools.Mediatypes.Data.Common.Models;

    public abstract class AbstractSearchableMediatypesRepository : ISearchableMediatypesRepository
    {
        public IEnumerable<IMediatype> GetByFileExtension(string fileExtension)
        {
            if (string.IsNullOrEmpty(fileExtension))
            {
                throw new ArgumentNullException(nameof(fileExtension));
            }

            var mediatype = this.GetMediatype(fileExtension);

            var result = this.MapStringToMediatypes(fileExtension, mediatype);
            return result;
        }

        protected abstract string GetMediatype(string fileExtension);

        private IEnumerable<IMediatype> MapStringToMediatypes(string fileExtension, string mediatype)
        {
            var result = new IMediatype[1];

            int slashIndex = mediatype.IndexOf('/');

            result[0] = new Mediatype
            {
                FileExtension = fileExtension,
                Mimetype = mediatype.Substring(0, slashIndex),
                Mimesubtype = mediatype.Substring(slashIndex + 1)
            };

            return result;
        }
    }
}
