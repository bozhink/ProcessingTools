namespace ProcessingTools.Mediatypes.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    using Models;

    public interface IMediatypesDataService
    {
        Task<IQueryable<MediatypeServiceModel>> GetMediaType(string fileExtension);
    }
}