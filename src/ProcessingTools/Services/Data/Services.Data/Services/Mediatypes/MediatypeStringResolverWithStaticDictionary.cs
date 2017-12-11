namespace ProcessingTools.Services.Data.Services.Mediatypes
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Services.Data.Mediatypes;

    public partial class MediatypeStringResolverWithStaticDictionary : IMediatypeStringResolver
    {
        public Task<string> ResolveAsync(string fileExtension)
        {
            string extension = fileExtension.TrimStart('.');
            this.mimetypes.TryGetValue(extension, out string mediatype);
            return Task.FromResult(mediatype);
        }
    }
}
