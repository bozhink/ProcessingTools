namespace ProcessingTools.Mediatypes.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    using Models;

    public interface IMediaTypeDataService
    {
        Task<IQueryable<MediaTypeServiceModel>> GetMediaType(string fileExtension);
    }
}