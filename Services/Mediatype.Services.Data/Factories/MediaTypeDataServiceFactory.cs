namespace ProcessingTools.MediaType.Services.Data.Factories
{
    using System.Linq;
    using System.Threading.Tasks;

    using Models;

    public abstract class MediaTypeDataServiceFactory : MediaTypeDataServiceBase
    {
        public override async Task<IQueryable<MediaTypeServiceModel>> GetMediaType(string fileExtension)
        {
            string extension = this.GetValidFileExtension(fileExtension);

            string mediaType = null;
            try
            {
                mediaType = await this.ResolveMediaType(extension);
            }
            catch
            {
                mediaType = MediaTypeDataServiceBase.DefaultMediaTypeOnException;
            }

            if (string.IsNullOrWhiteSpace(mediaType))
            {
                mediaType = MediaTypeDataServiceBase.DefaultMediaType;
            }

            return this.GetSingleStringMediaTypeResultAsQueryable(extension, mediaType);
        }

        protected abstract Task<string> ResolveMediaType(string fileExtension);
    }
}