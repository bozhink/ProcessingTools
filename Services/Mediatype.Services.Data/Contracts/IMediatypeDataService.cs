namespace ProcessingTools.MediaType.Services.Data.Contracts
{
    using System.Linq;

    public interface IMediaTypeDataService
    {
        IQueryable<IMediaType> GetMediaType(string fileExtension);
    }
}