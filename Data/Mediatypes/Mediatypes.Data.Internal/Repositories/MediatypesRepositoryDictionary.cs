namespace ProcessingTools.Mediatypes.Data.Internal.Repositories
{
    using Abstractions.Repositories;
    using ProcessingTools.Mediatypes.Data.Common.Contracts.Repositories;

    public partial class MediatypesRepositoryDictionary : AbstractMediatypesRepository, IMediatypesRepository
    {
        protected override string GetMediatype(string fileExtension)
        {
            string mediatype = null;
            if (this.mimetypes.TryGetValue(fileExtension, out mediatype))
            {
                return mediatype;
            }

            return null;
        }
    }
}
