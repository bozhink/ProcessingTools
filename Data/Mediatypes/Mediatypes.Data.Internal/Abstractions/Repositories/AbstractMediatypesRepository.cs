namespace ProcessingTools.Mediatypes.Data.Internal.Abstractions.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Mediatypes.Data.Common.Contracts.Models;
    using ProcessingTools.Mediatypes.Data.Common.Contracts.Repositories;
    using ProcessingTools.Mediatypes.Data.Common.Models;

    public abstract class AbstractMediatypesRepository : IMediatypesRepository
    {
        public Task<object> Add(IMediatype mediatype) => Task.FromResult<object>(false);

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

        public Task<object> Remove(string fileExtension) => Task.FromResult<object>(false);

        public Task<long> SaveChanges() => Task.FromResult(0L);

        public Task<object> UpdateDescription(string fileExtension, string description) => Task.FromResult<object>(false);

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
