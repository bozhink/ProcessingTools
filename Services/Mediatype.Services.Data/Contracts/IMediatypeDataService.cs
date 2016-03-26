namespace ProcessingTools.MediaType.Services.Data.Contracts
{
    using System.Linq;

    using Models.Contracts;

    public interface IMediaTypeDataService
    {
        IQueryable<IMediaTypeServiceModel> GetMediaType(string fileExtension);
    }
}