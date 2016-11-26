namespace ProcessingTools.Mediatypes.Data.Internal.Repositories
{
    using System.Threading.Tasks;
    using Abstractions.Repositories;
    using ProcessingTools.Mediatypes.Data.Common.Contracts.Repositories;

    public partial class MediatypesRepositoryDictionary : AbstractMediatypesRepository, IMediatypesRepository
    {
        protected override Task<string> GetMediatype(string fileExtension)
        {
            string mediatype = null;
            if (this.mimetypes.TryGetValue(fileExtension, out mediatype))
            {
                return Task.FromResult(mediatype);
            }

            return Task.FromResult<string>(null);
        }
    }
}
