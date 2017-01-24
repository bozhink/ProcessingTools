namespace ProcessingTools.Services.Data.Services.Mediatypes
{
    using System.Web;
    using Contracts.Mediatypes;
    using System.Threading.Tasks;

    public class MediatypeStringResolverWithSystemWeb : IMediatypeStringResolver
    {
        public Task<string> Resolve(string fileExtension)
        {
            string fileName = "/fake/file-name" + fileExtension;
            string mediatype = MimeMapping.GetMimeMapping(fileName);

            return Task.FromResult(mediatype);
        }
    }
}
