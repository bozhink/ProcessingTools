namespace ProcessingTools.MediaType.Services.Data.Factories
{
    using System.Linq;

    using Models.Contracts;

    public abstract class MediaTypeDataServiceFactory : MediaTypeDataServiceBase
    {
        public override IQueryable<IMediaTypeServiceModel> GetMediaType(string fileExtension)
        {
            string extension = this.GetValidFileExtension(fileExtension);

            string mediaType = null;
            try
            {
                mediaType = this.ResolveMediaType(extension);
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

        protected abstract string ResolveMediaType(string fileExtension);
    }
}