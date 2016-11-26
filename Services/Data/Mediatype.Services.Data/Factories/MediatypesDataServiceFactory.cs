namespace ProcessingTools.Mediatypes.Services.Data.Factories
{
    using System.Linq;
    using System.Threading.Tasks;

    using Models;

    public abstract class MediatypesDataServiceFactory : MediatypesDataServiceBase
    {
        public override async Task<IQueryable<MediatypeServiceModel>> GetMediaType(string fileExtension)
        {
            string extension = this.GetValidFileExtension(fileExtension);

            string mediaType = null;
            try
            {
                mediaType = await this.ResolveMediaType(extension);
            }
            catch
            {
                mediaType = MediatypesDataServiceBase.DefaultMediaTypeOnException;
            }

            if (string.IsNullOrWhiteSpace(mediaType))
            {
                mediaType = MediatypesDataServiceBase.DefaultMediaType;
            }

            return this.GetSingleStringMediaTypeResultAsQueryable(extension, mediaType);
        }

        protected abstract Task<string> ResolveMediaType(string fileExtension);
    }
}