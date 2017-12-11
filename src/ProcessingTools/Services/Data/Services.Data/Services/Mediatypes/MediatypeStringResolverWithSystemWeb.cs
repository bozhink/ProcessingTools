namespace ProcessingTools.Services.Data.Services.Mediatypes
{
    using System.Threading.Tasks;
    using System.Web;
    using ProcessingTools.Contracts.Services.Data.Mediatypes;

    public class MediatypeStringResolverWithSystemWeb : IMediatypeStringResolver
    {
        public Task<string> ResolveAsync(string fileExtension)
        {
            string fileName = "/fake/file-name" + fileExtension;
            string mediatype = MimeMapping.GetMimeMapping(fileName);

            return Task.FromResult(mediatype);
        }
    }
}
