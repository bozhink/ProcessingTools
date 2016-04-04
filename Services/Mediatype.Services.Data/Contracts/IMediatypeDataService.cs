namespace ProcessingTools.MediaType.Services.Data.Contracts
{
    using System.Linq;

    using Models;

    public interface IMediaTypeDataService
    {
        IQueryable<MediaTypeServiceModel> GetMediaType(string fileExtension);
    }
}