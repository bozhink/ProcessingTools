namespace ProcessingTools.Mediatypes.Data.Internal.Repositories
{
    using System.Web;
    using Abstractions.Repositories;
    using ProcessingTools.Mediatypes.Data.Common.Contracts.Repositories;

    public class MediatypesRepositorySystemWeb : AbstractMediatypesRepository, IMediatypesRepository
    {
        protected override string GetMediatype(string fileExtension)
        {
            string fileName = "/prefix/file." + fileExtension.Trim(new char[] { '.', ' ' });
            string mediatype = MimeMapping.GetMimeMapping(fileName);
            return mediatype;
        }
    }
}
