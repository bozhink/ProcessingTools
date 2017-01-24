namespace ProcessingTools.Services.Data.Services.Mediatypes
{
    using System.Threading.Tasks;
    using Contracts.Mediatypes;

    public partial class MediatypeStringResolverWithStaticDictionary : IMediatypeStringResolver
    {
        public Task<string> Resolve(string fileExtension)
        {
            string extension = fileExtension.TrimStart('.');
            string mediatype = null;
            this.mimetypes.TryGetValue(extension, out mediatype);
            return Task.FromResult(mediatype);
        }
    }
}
